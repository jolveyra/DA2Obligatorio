import { Component, OnDestroy, OnInit } from '@angular/core';
import { Building } from '../building.model';
import { BuildingService } from '../building.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthService } from '../../auth/auth.service';

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
