import { Component, OnDestroy, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthResponseData, AuthService } from './auth.service';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { UserLogged } from './userLogged.model';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.css'
})
export class AuthComponent {
  isLoading: boolean = false;
  error: string = "";

  constructor(private authService: AuthService, private router: Router) {}

  onSubmit(form: NgForm): void {
    this.error = "";

    if (!form.valid) {
      return;
    }

    this.isLoading = true;
    this.authService.login(form.value.email, form.value.password).subscribe(
      (responseData: AuthResponseData)  => {
        this.isLoading = false;
        this.authService.userLogged.next(new UserLogged(responseData.name, responseData.token, responseData.role));
        this.router.navigate(['/home']);
      },
      error => {
        console.log(error);
        this.error = error; // FIXME: Check for all cases
        this.isLoading = false;
      }
    )
  }
}
