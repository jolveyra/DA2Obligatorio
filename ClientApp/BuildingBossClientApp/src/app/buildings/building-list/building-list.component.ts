import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { AuthService } from '../../services/auth.service';
import { BuildingService } from '../../services/building.service';
import { ManagerService } from '../../services/manager.service';
import { ConstructorCompanyAdministratorService } from '../../services/constructor-company-administrator.service';
import { Building } from '../../shared/building.model';
import { User } from '../../shared/user.model';
import { ConstructorCompanyAdministrator } from '../../shared/constructor-company-administrator.model';
import { BuildingFlats } from '../../shared/buildingFlats.model';

@Component({
  selector: 'app-building-list',
  templateUrl: './building-list.component.html',
  styleUrl: './building-list.component.css'
})
export class BuildingListComponent implements OnInit, OnDestroy {
  userLoggedSub: Subscription = new Subscription();
  userRole: string = '';
  error: string = '';
  noBuildings: boolean = false;
  constructorCompanyName: boolean = true;
  finishedLoadingAdmin: boolean = false;
  buildings: Building[] | BuildingFlats[] = [];
  managers: User[] = [];
  constructorCompanyAdministrator: ConstructorCompanyAdministrator = new ConstructorCompanyAdministrator('', '', '', '', '', '');
  
  constructor(
    private authService: AuthService,
    private buildingService: BuildingService,
    private managerService: ManagerService,
    private constructorCompanyAdministratorService: ConstructorCompanyAdministratorService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.userLoggedSub = this.authService.userLogged.subscribe(user => this.userRole = user.role);
    if (this.userRole === 'Manager') {
      this.buildingService.fetchManagerBuildings().subscribe(
        response => {
          this.buildings = response;
          if (this.buildings.length === 0) {
            this.noBuildings = true;
          }
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
    if (this.userRole === 'ConstructorCompanyAdmin') {
      this.constructorCompanyAdministratorService.fetchConstructorCompanyAdministrator()
      .subscribe(
        
        constructorCompanyAdministrator => {
          this.constructorCompanyAdministrator = constructorCompanyAdministrator;
          if(this.constructorCompanyAdministrator.constructorCompanyName !== '') {
            this.buildingService.fetchConstructorCompanyBuildings().subscribe(
              
              buildingsResponse => {
                this.managerService.fetchManagers().subscribe(
                  managersResponse => {
                    this.managers = managersResponse;
                    this.buildings = buildingsResponse;
                    if (this.buildings.length === 0) {
                      this.noBuildings = true;
                    }
                    this.finishedLoadingAdmin = true;
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
          }else{
            this.constructorCompanyName = false;
            this.finishedLoadingAdmin = true;
          }
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
  }

  ngOnDestroy(): void {
    this.userLoggedSub.unsubscribe();
  }

  onNewBuilding() {
    this.router.navigate(['new'], { relativeTo: this.route });
  }
}
