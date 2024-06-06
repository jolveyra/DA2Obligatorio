import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { ManagerRequest } from '../request.model';
import { AuthService } from '../../services/auth.service';
import { RequestService } from '../../services/request.service';
import { Building } from '../../shared/building.model';
import { Flat } from '../../shared/flat.model';
import { User } from '../../shared/user.model';

@Component({
  selector: 'app-request-list',
  templateUrl: './request-list.component.html',
  styleUrl: './request-list.component.css'
})
export class RequestListComponent implements OnInit, OnDestroy {
  isLoading: boolean = false;
  successMessage: string = '';
  error: string = '';
  noRequests: boolean = false;
  endLoading: boolean = false;
  requests: ManagerRequest[] = [];
  userRole: string = '';
  userLoggedSub: Subscription = new Subscription();


  constructor(
    private authService: AuthService,
    private requestService: RequestService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.userLoggedSub = this.authService.userLogged.subscribe(userLogged => this.userRole = userLogged.role);
    if (this.userRole === 'Manager') {
      this.requestService.fetchManagerRequests().subscribe(
        requestsResponse => {
          this.requests = requestsResponse.map(request => {
            return new ManagerRequest(
              request.id,
              request.description,
              new Flat(
                request.flat.id,
                request.flat.buildingId,
                request.flat.number,
                request.flat.floor,
                request.flat.ownerName,
                request.flat.ownerSurname,
                request.flat.ownerEmail,
                request.flat.rooms,
                request.flat.bathrooms,
                request.flat.hasBalcony,
              ),
              new Building(
                request.building.id,
                request.building.name,
                request.building.sharedExpenses,
                request.building.street,
                request.building.doorNumber,
                request.building.cornerStreet,
                request.building.constructorCompanyId,
                request.building.managerId,
                request.building.latitude,
                request.building.longitude
              ),
              request.categoryName,
              new User(
                request.assignedEmployee.id,
                request.assignedEmployee.name,
                request.assignedEmployee.surname,
                request.assignedEmployee.email
              ),
              request.status,
            );
          });
          if (this.requests.length === 0) {
            this.noRequests = true;
          }
          this.endLoading = true;
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
    if (this.userRole === 'MaintenanceEmployee') {
      this.requestService.fetchMaintenanceEmployeeRequests().subscribe(
        requestsResponse => {
          this.requests = requestsResponse.map(request => {
            return new ManagerRequest(
              request.id,
              request.description,
              new Flat(
                request.flat.id,
                request.flat.buildingId,
                request.flat.number,
                request.flat.floor,
                request.flat.ownerName,
                request.flat.ownerSurname,
                request.flat.ownerEmail,
                request.flat.rooms,
                request.flat.bathrooms,
                request.flat.hasBalcony,
              ),
              new Building(
                request.building.id,
                request.building.name,
                request.building.sharedExpenses,
                request.building.street,
                request.building.doorNumber,
                request.building.cornerStreet,
                request.building.constructorCompanyId,
                request.building.managerId,
                request.building.latitude,
                request.building.longitude
              ),
              request.categoryName,
              undefined,
              request.status
            );
          });
          if (this.requests.length === 0) {
            this.noRequests = true;
          }
          this.endLoading = true;
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
  }

  ngOnDestroy(): void {
    this.userLoggedSub.unsubscribe();
  }

  onNewRequest(): void {
    this.router.navigate(['new'], { relativeTo: this.route });
  }

  updateStatus(event: { id: string, status: string }): void {
    this.isLoading = true;
    this.requestService.updateRequestStatus(event.id, event.status).subscribe(
      response => {
        this.successMessage = 'Request status updated successfully.';
        this.error = '';
        this.requests.find(request => request.id === response.id)!.status = response.status;
        this.isLoading = false;
      },
      error => {
        this.successMessage = '';
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
