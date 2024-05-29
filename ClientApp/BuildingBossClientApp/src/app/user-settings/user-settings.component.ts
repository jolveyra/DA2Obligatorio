import { Component, OnInit } from '@angular/core';
import { User } from '../shared/user.model';
import { UserSettingsService } from '../services/user-settings.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-user-settings',
  templateUrl: './user-settings.component.html',
  styleUrl: './user-settings.component.css'
})
export class UserSettingsComponent implements OnInit {
  isLoading: boolean = false;
  error: string = '';
  user: User = new User('', '', '', '');
  tempSurname: string = '';
  tempName: string = '';

  constructor(
    private userService: UserSettingsService
  ) {}

  ngOnInit(): void {
    this.user = this.userService.getUser();
    this.tempName = this.user.name;
    this.tempSurname = this.user.surname;
  }

  onSubmit(form: NgForm): void {
    this.userService.updateUser(form.value.name, form.value.surname, form.value.password);
  }
}
