import { Component, Input, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Flat } from '../building-flats/flat.model';
import { BuildingService } from '../building.service';
import { ActivatedRoute, Router } from '@angular/router';
import { BuildingFlats } from '../building-flats/buildingFlats.model';

@Component({
  selector: 'app-building-flat-edit',
  templateUrl: './building-flat-edit.component.html',
  styleUrl: './building-flat-edit.component.css'
})
export class BuildingFlatEditComponent implements OnInit {
  flat: Flat = new Flat('', 0, 0, '', '', '', 0, 0, false);
  isLoading: boolean = false;
  error: string = '';

  constructor(
    private buildingService: BuildingService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.isLoading = true;
    let building: BuildingFlats = this.buildingService.getBuildingFlats(this.route.snapshot.params['buildingId']);
    if (!building) {
      this.error = 'Building not found';
      return;
    } else {
      let buildingFlat = building.flats.find(f => f.id == this.route.snapshot.params['flatId']);
      this.flat = buildingFlat ? buildingFlat : new Flat('', 0, 0, '', '', '', 0, 0, false);
      if (!this.flat) {
        this.error = 'Flat not found';
        this.router.navigate(['../'], { relativeTo: this.route });
      }
      this.isLoading = false;
    }
  }

  onSubmit(form: NgForm): void {
    // FIXME: Implement this method
  }

  onBackIcon(): void {
    this.router.navigate(['../../'], { relativeTo: this.route });
    // const buildingId = this.route.snapshot.params['buildingId'];
    // if (buildingId) {
    //   this.router.navigate([`/buildings/${buildingId}`]);
    // } else {
    //   this.router.navigate(['/home']);
    // }
  }
}
