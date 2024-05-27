import { Component, OnDestroy, OnInit } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent implements OnInit, OnDestroy {
  userRole: string = 'Administrator'; //FIXME: Change this to empty string
  userLoggedSub: Subscription = new Subscription();
  
  constructor(private authService: AuthService) { }

  ngOnInit() {
    this.userLoggedSub = this.authService.userLogged.subscribe(userLogged => {
      this.userRole = userLogged.role;
    });
  }

  ngOnDestroy() {
    this.userLoggedSub.unsubscribe();
  }
}
