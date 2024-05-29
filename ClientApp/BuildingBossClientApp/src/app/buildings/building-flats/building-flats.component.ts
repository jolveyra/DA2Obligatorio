import { Component, Input } from '@angular/core';
import { Flat } from './flat.model';
import { ActivatedRoute, Router } from '@angular/router';
import { BuildingFlats } from './buildingFlats.model';
import { BuildingService } from '../../services/building.service';

@Component({
  selector: 'app-building-flats',
  templateUrl: './building-flats.component.html',
  styleUrl: './building-flats.component.css'
})
export class BuildingFlatsComponent {
  building: BuildingFlats = new BuildingFlats('', '', 0, [], '', 0, '', '', 0, 0);
  isLoading = false;

  constructor(
    private buildingService: BuildingService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.isLoading = true;
    this.building = this.buildingService.getBuildingFlats(this.route.snapshot.params['buildingId']); // FIXME: Add a loading spinner for when it's fetching the administrators
    this.isLoading = false;
  }
}
