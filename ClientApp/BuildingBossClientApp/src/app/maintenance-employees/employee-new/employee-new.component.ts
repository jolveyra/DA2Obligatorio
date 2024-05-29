import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { EmployeeService } from '../../services/employee.service';
import { User } from '../../shared/user.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employee-new',
  templateUrl: './employee-new.component.html',
  styleUrl: './employee-new.component.css'
})
export class EmployeeNewComponent {
  isLoading: boolean = false;
  error: string = '';

  constructor(
    private employeeService: EmployeeService,
    private router: Router
  ) {}

  onSubmit(form: NgForm): void {
    if (!form.valid) {
      return;
    }

    const newEmployee = new User(
      '',
      form.value.name,
      form.value.surname,
      form.value.email,
    );

    this.isLoading = true;
    this.employeeService.createMaintenanceEmployee(newEmployee, form.value.password)
      .subscribe(
        response => {
          this.isLoading = false;
          this.router.navigate(['/maintenanceEmployees']);
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
