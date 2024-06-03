import { Component } from '@angular/core';
import { ConstructorCompanyService } from '../services/constructor-company.service';

@Component({
  selector: 'app-constructor-companies',
  templateUrl: './constructor-companies.component.html',
  styleUrl: './constructor-companies.component.css'
})
export class ConstructorCompaniesComponent {
  isLoading: boolean = false;
  error: string = '';

  constructor(
    private constructorCompanyService: ConstructorCompanyService
  ) {}


}
