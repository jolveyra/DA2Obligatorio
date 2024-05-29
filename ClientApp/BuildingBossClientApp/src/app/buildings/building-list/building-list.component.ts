import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { AuthService } from '../../services/auth.service';
import { BuildingService } from '../../services/building.service';
import { Building } from '../../shared/building.model';

@Component({
  selector: 'app-building-list',
  templateUrl: './building-list.component.html',
  styleUrl: './building-list.component.css'
})
export class BuildingListComponent implements OnInit, OnDestroy {
  buildings: Building[] = [];
  isLoading = false;
  userLoggedSub: Subscription = new Subscription();
  userRole: string = '';

  constructor(
    private authService: AuthService,
    private buildingService: BuildingService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.isLoading = true;
    this.userLoggedSub = this.authService.userLogged.subscribe(user => this.userRole = user.role);
    this.buildings = this.buildingService.getBuildings(); // FIXME: Add a loading spinner for when it's fetching the administrators
    this.isLoading = false;
  }

  ngOnDestroy(): void {
    this.userLoggedSub.unsubscribe();
  }

  onNewBuilding() {
    this.router.navigate(['new'], { relativeTo: this.route });
  }
}
