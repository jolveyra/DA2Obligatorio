import { Component, OnInit } from '@angular/core';
import { User } from '../../shared/user.model';
import { ManagerService } from '../../services/manager.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-manager-list',
  templateUrl: './manager-list.component.html',
  styleUrl: './manager-list.component.css'
})
export class ManagerListComponent implements OnInit {
  managers: User[] = [];
  error: string = '';

  constructor(
    private managerService: ManagerService
  ) {}

  ngOnInit(): void {
    this.managerService.fetchManagers()
      .subscribe(
        response => {
          this.managers = response;
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
