import { Component } from '@angular/core';
import { ConstructorCompaniesService } from './constructor-company.service';

@Component({
  selector: 'app-constructor-companies',
  templateUrl: './constructor-companies.component.html',
  styleUrl: './constructor-companies.component.css'
})
export class ConstructorCompaniesComponent {
  isLoading: boolean = false;
  error: string = '';

  constructor(
    private constructorCompaniesService: ConstructorCompaniesService
  ) {}

  
}
