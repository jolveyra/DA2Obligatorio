import { Component, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BuildingFlats } from '../../shared/buildingFlats.model';
import { BuildingService } from '../../services/building.service';
import { NgForm } from '@angular/forms';
import { User } from '../../shared/user.model';
import { EmployeeService } from '../../services/employee.service';

@Component({
  selector: 'app-building-flats',
  templateUrl: './building-flats.component.html',
  styleUrl: './building-flats.component.css'
})
export class BuildingFlatsComponent {
  building: BuildingFlats = new BuildingFlats('', '', 0, [], '', 0, '', '', '', 0, 0, []);
  isLoading: boolean = false;
  error: string = '';
  successMessage: boolean = false;
  employeesOnBuilding: User[] = [];
  employeesNotOnBuilding: User[] = [];

  constructor(
    private buildingService: BuildingService,
    private employeeService: EmployeeService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.buildingService.fetchBuildingFlats(this.route.snapshot.params['buildingId']).subscribe(
      response => {
        this.building = response;
        this.employeeService.fetchMaintenanceEmployees().subscribe(
          response => {
            response.forEach(element => {
              if (this.building.maintenanceEmployeeIds.includes(element.id)) {
                this.employeesOnBuilding.push(element);
              } else {
                this.employeesNotOnBuilding.push(element);
              }
            });
          },
          error => {
            let errorMessage = "An unexpected error has occured, please retry later."
            if (error.error && error.error.errorMessage) {
              this.error = error.error.errorMessage;
            } else {
              this.error = errorMessage;
            }
          }
        );
      },
      error => {
        let errorMessage = "An unexpected error has occured, please retry later."
        if (error.error && error.error.errorMessage) {
          this.error = error.error.errorMessage;
        } else {
          this.error = errorMessage;
        }
      }
    );
  }

  onAdd(employee: User) {
    const employeeToAdd = this.employeesNotOnBuilding.splice(this.employeesNotOnBuilding.indexOf(employee), 1);
    this.employeesOnBuilding.push(employeeToAdd[0]);
  }

  onDiscard(employee: User) {
    const employeeToDiscard = this.employeesOnBuilding.splice(this.employeesOnBuilding.indexOf(employee), 1);
    this.employeesNotOnBuilding.push(employeeToDiscard[0]);
  }

  onSubmit(form: NgForm) {
    if (!form.valid) {
      return;
    }

    this.isLoading = true;
    this.buildingService.updateBuilding(this.route.snapshot.params['buildingId'], form.value.sharedExpenses, this.employeesOnBuilding.map(employee => employee.id))
      .subscribe(
        response => {
          this.isLoading = false;
          this.building = response;
          this.error = '';
          this.successMessage = true;
        },
        error => {
          let errorMessage = "An unexpected error has occured, please retry later."
          if (error.error && error.error.errorMessage) {
            this.error = error.error.errorMessage;
          } else {
            this.error = errorMessage;
          }
          this.isLoading = false;
        }
      );
  }
}
