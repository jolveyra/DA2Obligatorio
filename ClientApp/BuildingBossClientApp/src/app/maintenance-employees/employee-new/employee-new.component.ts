import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-employee-new',
  templateUrl: './employee-new.component.html',
  styleUrl: './employee-new.component.css'
})
export class EmployeeNewComponent {
  isLoading: boolean = false;
  error: string = '';

  onSubmit(form: NgForm): void {

  }
}
