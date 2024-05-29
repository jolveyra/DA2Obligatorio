import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit, OnDestroy {
  userLoggedSub: Subscription = new Subscription();
  isAuthenticated: boolean = true; // FIXME: change to false

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.authService.autoLogin();
    this.userLoggedSub = this.authService.userLogged
      .subscribe(userLogged => {
        this.isAuthenticated = userLogged.token === '' ? false : true;
      });
  }

  ngOnDestroy(): void {
    this.userLoggedSub.unsubscribe();
  }
}
