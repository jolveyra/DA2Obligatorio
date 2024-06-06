import { ConstructorCompanyAdministratorService } from './../../services/constructor-company-administrator.service';
import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { BuildingService } from '../../services/building.service';
import { Building } from '../../shared/building.model';
import { AuthService } from '../../services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-building-new',
  templateUrl: './building-new.component.html',
  styleUrl: './building-new.component.css'
})
export class BuildingNewComponent {
  isLoading: boolean = false;
  error: string = '';
  userId: string = '';
  userLoggedSub: Subscription = new Subscription();
  constructorCompany = {
    id: '',
    name: ''
  };

  constructor(
    private buildingService: BuildingService,
    private constructorCompanyAdministratorService: ConstructorCompanyAdministratorService,
    private router: Router,
    private route: ActivatedRoute,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.userLoggedSub = this.authService.userLogged.subscribe(userLogged => {
      this.userId = userLogged.id;
      this.constructorCompanyAdministratorService.fetchConstructorCompanyAdministrator(this.userId)
      .subscribe(
        constructorCompanyAdministrator => {
          this.constructorCompany.name = constructorCompanyAdministrator.constructorCompanyName;
          this.constructorCompany.id = constructorCompanyAdministrator.constructorCompanyId;
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
    });
    
  }
  
  onSubmit(form: NgForm): void {
    
    this.isLoading = true;

    const building: Building = new Building(
      '',
      form.value.name,
      form.value.sharedExpenses,
      form.value.street,
      form.value.doorNumber,
      form.value.cornerStreet,
      this.constructorCompany.id,
      '',
      form.value.latitude,
      form.value.longitude
    );

    this.buildingService.createConstructorCompanyBuilding(building, form.value.amountOfFlats)
    .subscribe(
      response => {
        this.isLoading = false;
        this.router.navigate(['/buildings'], { relativeTo: this.route });
      },
      error => {
        let errorMessage = "An unexpected error has occured, please retry later."
        this.isLoading = false;
        if (error.error && error.error.errorMessage) {
          this.error = error.error.errorMessage;
        } else {
          this.error = errorMessage;
        }
      }
    );
    
    
  }
}
