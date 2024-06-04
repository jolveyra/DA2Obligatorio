import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent implements OnInit, OnDestroy {
  userLoggedSub: Subscription = new Subscription();
  name: string = '';
  
  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.userLoggedSub = this.authService.userLogged.subscribe(
      userLogged => {
        this.name = userLogged.name;
      }
    );
  }

  ngOnDestroy(): void {
    this.userLoggedSub.unsubscribe();
  }

  onLogout(): void {
    this.authService.logout();
  }

  onUserSettings(): void {
    this.router.navigate(['userSettings'])
  }
}
