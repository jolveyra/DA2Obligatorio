import { Component } from '@angular/core';
import { ConstructorCompanyService } from '../../services/constructor-company.service';
import { ConstructorCompany } from '../../shared/constructor-company.model';
import { ConstructorCompanyAdministratorService } from '../../services/constructor-company-administrator.service';
import { Subscription } from 'rxjs';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-constructor-company-list',
  templateUrl: './constructor-company-list.component.html',
  styleUrl: './constructor-company-list.component.css'
})
export class ConstructorCompanyListComponent {

  constructorCompanies: ConstructorCompany[] = [];
  error: string = '';
  noConstructorCompanies: boolean = false;
  constructorCompanyToAssign: ConstructorCompany = new ConstructorCompany('', '');
  userId: string = '';
  userLoggedSub: Subscription = new Subscription();
  
  constructor(
    private constructorCompanyService: ConstructorCompanyService,
    private constructorCompanyAdministratorService: ConstructorCompanyAdministratorService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.constructorCompanyService.fetchConstructorCompanies()
    .subscribe(
      response => {
        this.constructorCompanies = response;
        if (this.constructorCompanies.length === 0) {
          this.noConstructorCompanies = true;
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
  
    this.userLoggedSub = this.authService.userLogged.subscribe(userLogged => {
      this.userId = userLogged.id;
    });
  }

  ngOnDestroy() {
    this.userLoggedSub.unsubscribe();
  }

  onSelectConstructorCompany(id: string): void {
    this.constructorCompanyService.fetchConstructorCompany(id)
    .subscribe(
      response => {
        this.constructorCompanyToAssign = response;
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

  onAssingConstructorCompany(): void {

    //this.constructorCompanyAdministratorService.assignConstructorCompany(this.constructorCompanyToAssign)
  }

  
}