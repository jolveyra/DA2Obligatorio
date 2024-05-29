import { Component, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BuildingFlats } from '../../shared/buildingFlats.model';
import { BuildingService } from '../../services/building.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-building-flats',
  templateUrl: './building-flats.component.html',
  styleUrl: './building-flats.component.css'
})
export class BuildingFlatsComponent {
  building: BuildingFlats = new BuildingFlats('', '', 0, [], '', 0, '', '', 0, 0);
  isLoading: boolean = false;
  error: string = '';

  constructor(
    private buildingService: BuildingService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.buildingService.fetchBuildingFlats(this.route.snapshot.params['buildingId']).subscribe(
      response => {
        this.building = response;
        // this.buildingService.setBuildingFlats(response);
        // this.building = this.buildingService.getBuildingFlats();
      },
      error => {
        console.log(error);
        let errorMessage = "An unexpected error has occured, please retry later."
        if (error.error && error.error.errorMessage) {
          this.error = error.error.errorMessage;
        } else {
          this.error = errorMessage;
        }
      }
    );;
  }

  onSubmit(form: NgForm) {}
}
