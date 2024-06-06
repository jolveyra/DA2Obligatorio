import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { ManagerRequest } from '../request.model';
import { RequestService } from '../../services/request.service';
import { Flat } from '../../shared/flat.model';
import { Building } from '../../shared/building.model';
import { User } from '../../shared/user.model';

@Component({
  selector: 'app-request-edit',
  templateUrl: './request-edit.component.html',
  styleUrl: './request-edit.component.css'
})
export class RequestEditComponent implements OnInit {
  request: ManagerRequest = new ManagerRequest('', '', new Flat('', '', 0, 0, '', '', '', 0, 0, false), new Building('', '', 0, '', 0, '', '', '', 0, 0), '', new User('', '', '', ''), '');
  isLoading: boolean = false;
  error: string = '';
  categories = ['Category 1', 'Category 2', 'Category 3'];
  selectedCategory: string = '';
  employees = ['Employee 1', 'Employee 2', 'Employee 3'];
  selectedEmployee: string = '';
  description: string = '';

  constructor(
    private requestService: RequestService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.requestService.fetchManagerRequest(this.route.snapshot.params['id'])
      .subscribe(
        response => {
          this.request = new ManagerRequest(
            response.id,
            response.description,
            new Flat(
              response.flat.id,
              response.flat.buildingId,
              response.flat.number,
              response.flat.floor,
              response.flat.ownerName,
              response.flat.ownerSurname,
              response.flat.ownerEmail,
              response.flat.rooms,
              response.flat.bathrooms,
              response.flat.hasBalcony,
            ),
            new Building(
              response.building.id,
              response.building.name,
              response.building.sharedExpenses,
              response.building.street,
              response.building.doorNumber,
              response.building.cornerStreet,
              response.building.constructorCompanyId,
              response.building.managerId,
              response.building.latitude,
              response.building.longitude,
            ),
            response.categoryName,
            new User(
              response.assignedEmployee.id,
              response.assignedEmployee.name,
              response.assignedEmployee.surname,
              response.assignedEmployee.email,
            ),
            response.status,
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

  selectEmployee(employee: string) {
      this.selectedCategory = employee;
  }

  onSubmit(form: NgForm): void {
    // FIXME: Implement this method
  }
}
