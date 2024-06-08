import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { BuildingService } from '../../services/building.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryService } from '../../services/category.service';
import { EmployeeService } from '../../services/employee.service';
import { BuildingFlats } from '../../shared/buildingFlats.model';
import { User } from '../../shared/user.model';
import { Flat } from '../../shared/flat.model';
import { RequestService } from '../../services/request.service';
import { Request } from '../request.model';

@Component({
  selector: 'app-request-new',
  templateUrl: './request-new.component.html',
  styleUrl: './request-new.component.css'
})
export class RequestNewComponent implements OnInit {
  isLoading: boolean = false;
  error: string = '';
  noCategories: boolean = false;
  noBuildings: boolean = false;
  noEmployees: boolean = false;
  categories: string[] = [];
  selectedCategory: string = '';
  buildings: BuildingFlats[] = [];
  selectedFlat: string = '';
  allEmployees: User[] = [];
  employeesToShow: User[] = [];
  selectedEmployee: string = '';
  formFlat: Flat = new Flat('', '', 0, 0, '', '', '', 0, 0, true);
  formEmployee: User = new User('', '', '', '');

  constructor(
    private buildingService: BuildingService,
    private categoryService: CategoryService,
    private employeeService: EmployeeService,
    private requestService: RequestService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.categoryService.fetchCategories().subscribe(
      categoriesResponse => {
        this.buildingService.fetchManagerBuildings().subscribe(
          buildingsResponse => {
            this.employeeService.fetchMaintenanceEmployees().subscribe(
              employeeResponse => {
                
                this.categories = categoriesResponse.map(category => category.name);
                this.buildings = buildingsResponse;
                this.allEmployees = employeeResponse;

                if (this.categories.length === 0) {
                  this.noCategories = true;
                }
                if (this.buildings.length === 0) {
                  this.noBuildings = true;
                }
                if (this.allEmployees.length === 0) {
                  this.noEmployees = true;
                }
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

  selectCategory(category: string): void {
    this.selectedCategory = category;
  }

  getFlatName(flat: string, building: string): string {
    return flat + ' - ' + building;
  }

  selectFlat(flat: Flat): void {
    this.formFlat = flat;
    let building = this.buildings.find(building => building.id === flat.buildingId);
    this.selectedFlat = flat.number + ' - ' + building!.name;
    this.employeesToShow = this.allEmployees.filter(employee => building?.maintenanceEmployeeIds.includes(employee.id));
  }

  selectEmployee(employee: User): void {
    this.formEmployee = employee;
    this.selectedEmployee = employee.name + ' ' + employee.surname + ' - ' + employee.email;
  }

  onSubmit(form: NgForm): void {
    if (!form.valid) {
      return;
    }

    let request = new Request(
      '',
      form.value.description,
      this.formFlat,
      this.buildings.find(building => building.id === this.formFlat.buildingId)!,
      this.selectedCategory,
      this.formEmployee,
      ''
    );

    this.isLoading = true;
    this.error = '';

    this.requestService.createRequest(request).subscribe(
      response => {
        this.isLoading = false;
        this.router.navigate(['/requests']);
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
