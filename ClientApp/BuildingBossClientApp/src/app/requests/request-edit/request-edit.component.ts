import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { Request } from '../request.model';
import { RequestService } from '../../services/request.service';
import { Flat } from '../../shared/flat.model';
import { Building } from '../../shared/building.model';
import { User } from '../../shared/user.model';
import { CategoryService } from '../../services/category.service';
import { EmployeeService } from '../../services/employee.service';
import { BuildingService } from '../../services/building.service';

@Component({
  selector: 'app-request-edit',
  templateUrl: './request-edit.component.html',
  styleUrl: './request-edit.component.css'
})
export class RequestEditComponent implements OnInit {
  request: Request = new Request('', '', new Flat('', '', 0, 0, '', '', '', 0, 0, false), new Building('', '', 0, '', 0, '', '', '', 0, 0), '', new User('', '', '', ''), '');
  isLoading: boolean = false;
  error: string = '';
  categories: string[] = [];
  selectedCategory: string = '';
  employees: User[] = [];
  selectedEmployee: string = '';
  description: string = '';

  constructor(
    private requestService: RequestService,
    private categoryService: CategoryService,
    private buildingService: BuildingService,
    private employeeService: EmployeeService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.requestService.fetchManagerRequest(this.route.snapshot.params['id'])
      .subscribe(
        requestResponse => {
          this.categoryService.fetchCategories().subscribe(
            categoriesResponse => {
              this.categories = categoriesResponse.map(category => category.name);
              this.request = new Request(
                requestResponse.id,
                requestResponse.description,
                new Flat(
                  requestResponse.flat.id,
                  requestResponse.flat.buildingId,
                  requestResponse.flat.number,
                  requestResponse.flat.floor,
                  requestResponse.flat.ownerName,
                  requestResponse.flat.ownerSurname,
                  requestResponse.flat.ownerEmail,
                  requestResponse.flat.rooms,
                  requestResponse.flat.bathrooms,
                  requestResponse.flat.hasBalcony,
                ),
                new Building(
                  requestResponse.building.id,
                  requestResponse.building.name,
                  requestResponse.building.sharedExpenses,
                  requestResponse.building.street,
                  requestResponse.building.doorNumber,
                  requestResponse.building.cornerStreet,
                  requestResponse.building.constructorCompanyId,
                  requestResponse.building.managerId,
                  requestResponse.building.latitude,
                  requestResponse.building.longitude,
                ),
                requestResponse.categoryName,
                new User(
                  requestResponse.assignedEmployee.id,
                  requestResponse.assignedEmployee.name,
                  requestResponse.assignedEmployee.surname,
                  requestResponse.assignedEmployee.email,
                ),
                requestResponse.status,
              );
              this.buildingService.fetchManagerBuilding(this.request.building.id).subscribe(
                buildingResponse => {
                  this.employeeService.fetchMaintenanceEmployees().subscribe(
                    employeesResponse => {
                      this.employees = employeesResponse.filter(employee => buildingResponse.maintenanceEmployeeIds.includes(employee.id));
                      this.selectedCategory = this.request.categoryName;
                      this.description = this.request.description;
                      if (this.request.assignedEmployee) {
                        this.selectedEmployee = this.request.assignedEmployee.name + ' ' + this.request.assignedEmployee.surname + ' - ' + this.request.assignedEmployee.email;
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

  selectCategory(category: string) {
      this.selectedCategory = category;
  }

  selectEmployee(employee: User) {
      this.selectedEmployee = employee.name + ' ' + employee.surname + ' - ' + employee.email;
  }

  onSubmit(form: NgForm): void {
    // FIXME: Implement this method
  }
}
