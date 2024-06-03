import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../../shared/user.model';
import { EmployeeService } from '../../services/employee.service';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrl: './employee-list.component.css'
})
export class EmployeeListComponent {
  maintenanceEmployees: User[] = [];
  error: string = '';
  noMaintenanceEmployees: boolean = false;

  constructor(
    private employeeService: EmployeeService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.employeeService.fetchMaintenanceEmployees()
      .subscribe(
        response => {
          this.maintenanceEmployees = response;
          if (this.maintenanceEmployees.length === 0) {
            this.noMaintenanceEmployees = true;
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

  onNewMaintenanceEmployee(): void {
    this.router.navigate(['new'], {relativeTo: this.route});
  }
}
