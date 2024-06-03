import { Component, OnInit } from '@angular/core';
import { User } from '../shared/user.model';
import { UserSettingsService } from '../services/user-settings.service';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-settings',
  templateUrl: './user-settings.component.html',
  styleUrl: './user-settings.component.css'
})
export class UserSettingsComponent implements OnInit {
  isLoading: boolean = false;
  error: string = '';
  user: User = new User('', '', '', '');

  constructor(
    private userService: UserSettingsService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.userService.fetchUser()
      .subscribe(
        reponse => {
          this.user = reponse;
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

  onSubmit(form: NgForm): void {
    this.userService.updateUser(form.value.name, form.value.surname, form.value.password)
      .subscribe(
        response => {
          this.isLoading = false;
          this.router.navigate(['/home']);
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
}
