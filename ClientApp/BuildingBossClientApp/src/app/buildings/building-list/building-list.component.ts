import { Component } from '@angular/core';
import { Building } from '../building.model';
import { BuildingService } from '../building.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-building-list',
  templateUrl: './building-list.component.html',
  styleUrl: './building-list.component.css'
})
export class BuildingListComponent {
  buildings: Building[] = [];
  isLoading = false;

  constructor(
    private buildingService: BuildingService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.isLoading = true;
    this.buildings = this.buildingService.getBuildings(); // FIXME: Add a loading spinner for when it's fetching the administrators
    this.isLoading = false;
  }

  onNewBuilding(): void {
    this.router.navigate(['new'], {relativeTo: this.route});
  }
}
