import { ConstructorCompanyAdministrator } from './../shared/constructor-company-administrator.model';
import { ConstructorCompany } from './../shared/constructor-company.model';
import { Component } from '@angular/core';

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


}

