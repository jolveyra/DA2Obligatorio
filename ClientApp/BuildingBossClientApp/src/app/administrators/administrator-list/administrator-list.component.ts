import { Component, OnInit } from '@angular/core';
import { User } from '../../shared/user.model';
import { AdministratorService } from '../../services/administrator.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-administrator-list',
  templateUrl: './administrator-list.component.html',
  styleUrl: './administrator-list.component.css'
})
export class AdministratorListComponent implements OnInit {
  administrators: User[] = [];
  error: string = '';
  noAdministrators: boolean = false;

  constructor(
    private administratorService: AdministratorService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.administratorService.fetchAdministrators()
      .subscribe(
        response => {
          this.administrators = response;
          if (this.administrators.length === 0) {
            this.noAdministrators = true;
          }
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

  onNewAdministrator(): void {
    this.router.navigate(['new'], {relativeTo: this.route});
  }
}
