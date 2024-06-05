import { ConstructorCompanyAdministrator } from './../shared/constructor-company-administrator.model';
import { ConstructorCompany } from './../shared/constructor-company.model';
import { Component } from '@angular/core';
import { ConstructorCompanyService } from '../services/constructor-company.service';
import { ConstructorCompanyAdministratorService } from '../services/constructor-company-administrator.service';

@Component({
  selector: 'app-constructor-companies',
  templateUrl: './constructor-companies.component.html',
  styleUrl: './constructor-companies.component.css'
})
export class ConstructorCompaniesComponent {
  isLoading: boolean = true;
  error: string = '';
  
  constructorCompanyAdministrator: ConstructorCompanyAdministrator = new ConstructorCompanyAdministrator('', '', '', '', '', '');
  constructorCompany: ConstructorCompany = new ConstructorCompany('', '');

  constructor(
    private constructorCompanyService: ConstructorCompanyService,
    private constructorCompanyAdministratorService: ConstructorCompanyAdministratorService
  ) {}

  
  ngOnInit(): void {

    this.constructorCompanyAdministratorService.fetchConstructorCompanyAdministrator()
      .subscribe(
        constructorCompanyAdministrator => {

          this.constructorCompanyAdministrator = constructorCompanyAdministrator;
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

