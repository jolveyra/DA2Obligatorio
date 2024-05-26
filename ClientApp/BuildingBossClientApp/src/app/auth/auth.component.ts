import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthResponseData, AuthService } from './auth.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.css'
})
export class AuthComponent {
  isLoading: boolean = false;
  error: string = "";

  constructor(private authService: AuthService) {}

  onSubmit(form: NgForm) {
    this.error = "";

    if (!form.valid) {
      return;
    }

    this.isLoading = true;
    // FIXME: Call the AuthService to login, correct whats after
    // try {
    //   const response = this.authService.login(form.value.email, form.value.password);
    //   response.subscribe(response => {
    //       console.log(response);
    //   });
    //   this.isLoading = false;
    // } catch (error) {
    //   console.log(error);
    //   this.isLoading = false;
    // }
    this.isLoading = false; // Delete when fixed whats above
  }
}
