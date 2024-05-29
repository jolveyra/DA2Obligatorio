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
  userLoggedSub: Subscription = new Subscription();
  userRole: string = '';
  isLoading = false;
  error: string = '';
  buildings: Building[] = [];

  constructor(
    private authService: AuthService,
    private buildingService: BuildingService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.userLoggedSub = this.authService.userLogged.subscribe(user => this.userRole = user.role);
    this.buildingService.fetchBuildings().subscribe(
      response => {
        console.log(response);
        this.buildingService.setBuildings(response);
        this.buildings = this.buildingService.getBuildings();
        this.isLoading = false;
      },
      error => {
        let errorMessage = "An unexpected error has occured, please retry later."
        if (error.error && error.error.errorMessage) {
          this.error = error.error.errorMessage;
        } else {
          this.error = errorMessage;
        }
        this.isLoading = false;
      }
    );
  }

  ngOnDestroy(): void {
    this.userLoggedSub.unsubscribe();
  }

  onNewBuilding() {
    this.router.navigate(['new'], { relativeTo: this.route });
  }
}
