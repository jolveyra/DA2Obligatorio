import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { Request } from '../request.model';
import { AuthService } from '../../services/auth.service';
import { RequestResponseData, RequestService } from '../../services/request.service';
import { BuildingService } from '../../services/building.service';
import { EmployeeService } from '../../services/employee.service';

@Component({
  selector: 'app-request-list',
  templateUrl: './request-list.component.html',
  styleUrl: './request-list.component.css'
})
export class RequestListComponent implements OnInit, OnDestroy {
  isLoading: boolean = false;
  error: string = '';
  requests: Request[] = [];
  maintenanceEmployees: string[] = [];
  userRole: string = '';
  userLoggedSub: Subscription = new Subscription();

  constructor(
    private authService: AuthService,
    private requestService: RequestService,
    private buildingService: BuildingService,
    private employeeService: EmployeeService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.userLoggedSub = this.authService.userLogged.subscribe(userLogged => this.userRole = userLogged.role);
    this.requestService.fetchRequests().subscribe(
      (response: RequestResponseData[]) => {
        // this.buildingService.fetchBuildings().subscribe(); 
        // this.employeeService.fetchMaintenanceEmployees().subscribe(); // FIXME: have to make it reactive to which employees could it be assigned depending on the building
        // this.requests = response.map(request => new Request());
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
