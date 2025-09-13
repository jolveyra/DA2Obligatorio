import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Flat } from '../../shared/flat.model';
import { BuildingService } from '../../services/building.service';

@Component({
  selector: 'app-building-flat-edit',
  templateUrl: './building-flat-edit.component.html',
  styleUrl: './building-flat-edit.component.css'
})
export class BuildingFlatEditComponent implements OnInit {
  flat: Flat = new Flat('', '', 0, 0, '', '', '', 0, 0, false);
  isLoading: boolean = false;
  error: string = '';
  isChangingOwner: boolean = false;

  constructor(
    private buildingService: BuildingService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.buildingService.fetchFlat(this.route.snapshot.params['buildingId'], this.route.snapshot.params['flatId'])
      .subscribe(
        response => {
          this.flat = response;
        },
        error => {
          let errorMessage = "An unexpected error has occured, please retry later."
          if (error.error && error.error.errorMessage) {
            this.error = error.error.errorMessage;
          } else {
            this.error = errorMessage;
          }
        }
      );
  }

  onSubmit(form: NgForm): void {
    if (!form.valid) { // Another check just in case
      return;
    }

    let updatedFlat = new Flat(
      this.flat.id,
      this.flat.buildingId,
      form.value.doorNumber,
      form.value.floor,
      this.flat.ownerName,
      this.flat.ownerSurname,
      this.flat.ownerEmail,
      form.value.rooms,
      form.value.bathrooms,
      form.value.hasBalcony
    );

    this.isLoading = true;
    this.error = '';
    this.buildingService.updateFlat(
      updatedFlat,
      form.value.changeOwner).subscribe(
        response => {
          this.isLoading = false;
          this.router.navigate(['../../'], { relativeTo: this.route });
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

  onBackIcon(): void {
    this.router.navigate(['../../'], { relativeTo: this.route });
  }
}
