import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { User } from '../../shared/user.model';
import { AdministratorService } from '../../services/administrator.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-administrator-new',
  templateUrl: './administrator-new.component.html',
  styleUrl: './administrator-new.component.css'
})
export class AdministratorNewComponent {
  isLoading: boolean = false;
  error: string = '';

  constructor(
    private administratorService: AdministratorService,
    private router: Router
  ) {}

  onSubmit(form: NgForm): void {
    if (!form.valid) {
      return;
    }
    this.isLoading = true;
    this.error = '';

    const newAdministrator = new User(
      '',
      form.value.name,
      form.value.surname,
      form.value.email
    );

    this.administratorService.createAdministrator(newAdministrator, form.value.password)
      .subscribe(
        response => {
          this.isLoading = false;
          this.router.navigate(['/administrators']);
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
