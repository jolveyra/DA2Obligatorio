import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { Request } from '../request.model';
import { AuthService } from '../../services/auth.service';
import { RequestService } from '../../services/request.service';

@Component({
  selector: 'app-request-list',
  templateUrl: './request-list.component.html',
  styleUrl: './request-list.component.css'
})
export class RequestListComponent implements OnInit, OnDestroy {
  isLoading: boolean = false;
  error: string = '';
  requests: Request[] = [];
  userRole: string = '';
  userLoggedSub: Subscription = new Subscription();

  constructor(
    private authService: AuthService,
    private requestService: RequestService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.isLoading = true;
    this.userLoggedSub = this.authService.userLogged.subscribe(userLogged => this.userRole = userLogged.role);
    this.requests = this.requestService.getRequests();
    this.isLoading = false;
  }

  ngOnDestroy(): void {
    this.userLoggedSub.unsubscribe();
  }

  onNewRequest(): void {
    this.router.navigate(['new'], { relativeTo: this.route });
  }
}
