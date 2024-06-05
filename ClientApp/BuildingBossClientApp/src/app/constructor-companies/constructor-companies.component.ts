import { ConstructorCompanyAdministrator } from './../shared/constructor-company-administrator.model';
import { ConstructorCompany } from './../shared/constructor-company.model';
import { Component } from '@angular/core';
import { ConstructorCompanyAdministratorService } from '../services/constructor-company-administrator.service';
import { ConstructorCompanyService } from '../services/constructor-company.service';

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
  constructorCompanyName: boolean = false;

  constructor(
    private constructorCompanyAdministratorService: ConstructorCompanyAdministratorService,
    private constructorCompanyService: ConstructorCompanyService
  ) {}

  
  ngOnInit(): void {

    this.constructorCompanyAdministratorService.fetchConstructorCompanyAdministrator()
      .subscribe(
        constructorCompanyAdministrator => {
          this.constructorCompanyAdministrator = constructorCompanyAdministrator;
          
          if(this.constructorCompanyAdministrator.constructorCompanyName !== '') {
            this.constructorCompanyName = true;
            this.constructorCompany = new ConstructorCompany(this.constructorCompanyAdministrator.constructorCompanyId, this.constructorCompanyAdministrator.constructorCompanyName);
          }
          this.isLoading = false;
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

  onEditConstructorCompany(): void {
    console.log(this.constructorCompany.name)

    this.constructorCompanyService.updateConstructorCompany(this.constructorCompany)
    .subscribe(
      response => {
        console.log(response);
        this.constructorCompany.name = response.name;
        this.constructorCompany.id = response.id;
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

