import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from './auth.service';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.css'
})
export class AuthComponent {
  authSub = Subscription;
  isLoading: boolean = false;
  error: string = "";

  constructor(private authService: AuthService, private router: Router) {}

  onSubmit(form: NgForm): void {
    this.error = "";

    if (!form.valid) {
      return;
    }

    this.isLoading = true;
    // FIXME: Call the AuthService to login, correct whats after
    this.authService.login(form.value.email, form.value.password).subscribe(
      responseData => {
        console.log(responseData);
        this.isLoading = false;
      },
      error => {
        console.log(error);
        this.error = error;
        this.isLoading = false;
        // this.router.navigate(['/home']);  // FIXME: Add to successful case
      }
    )
  }
}
