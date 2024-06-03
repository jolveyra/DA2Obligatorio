import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { Request } from '../request.model';
import { AuthService } from '../../services/auth.service';
import { RequestService } from '../../services/request.service';
import { BuildingService } from '../../services/building.service';
import { EmployeeService } from '../../services/employee.service';
import { User } from '../../shared/user.model';
import { BuildingFlats } from '../../shared/buildingFlats.model';
import { Building } from '../../shared/building.model';

@Component({
  selector: 'app-request-list',
  templateUrl: './request-list.component.html',
  styleUrl: './request-list.component.css'
})
export class RequestListComponent implements OnInit, OnDestroy {
  isLoading: boolean = false;
  error: string = '';
  noRequests: boolean = false;
  requests: Request[] = [];
  userRole: string = '';
  userLoggedSub: Subscription = new Subscription();

  constructor(
    private authService: AuthService,
    private requestService: RequestService,
    private buildingService: BuildingService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.userLoggedSub = this.authService.userLogged.subscribe(userLogged => this.userRole = userLogged.role);
    this.requestService.fetchRequests().subscribe(
      requestsResponse => {
        this.requests = requestsResponse.map(request => {
          return new Request(
            request.id,
            request.description,
            request.flatNumber,
            request.buildingName,
            request.categoryName,
            request.assignedEmployee,
            request.status,
          );
        });
        if (this.requests.length === 0) {
          this.noRequests = true;
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
  }

  ngOnDestroy(): void {
    this.userLoggedSub.unsubscribe();
  }

  onNewRequest(): void {
    this.router.navigate(['new'], { relativeTo: this.route });
  }
}
