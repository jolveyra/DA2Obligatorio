import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-administrator-new',
  templateUrl: './administrator-new.component.html',
  styleUrl: './administrator-new.component.css'
})
export class AdministratorNewComponent {
  isLoading: boolean = false;
  error: string = '';

  onSubmit(form: NgForm): void {

  }
}
