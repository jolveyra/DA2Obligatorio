import { Component, OnInit } from '@angular/core';
import { User } from '../../shared/user.model';
import { AdministratorService } from '../administrator.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-administrator-list',
  templateUrl: './administrator-list.component.html',
  styleUrl: './administrator-list.component.css'
})
export class AdministratorListComponent implements OnInit {
  administrators: User[] = [];

  constructor(
    private administratorService: AdministratorService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.administrators = this.administratorService.getAdministrators();
  }

  onNewRecipe(): void {
    this.router.navigate(['new'], {relativeTo: this.route});
  }
}
