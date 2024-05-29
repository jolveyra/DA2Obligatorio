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

  constructor(
    private employeeService: EmployeeService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.maintenanceEmployees = this.employeeService.getMaintenanceEmployees(); // FIXME: Add a loading spinner for when it's fetching the maintenanceEmployees
  }

  onNewMaintenanceEmployee(): void {
    this.router.navigate(['new'], {relativeTo: this.route});
  }
}
