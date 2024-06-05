import { Component } from '@angular/core';
import { ConstructorCompanyService } from '../../services/constructor-company.service';

@Component({
  selector: 'app-constructor-company-list',
  templateUrl: './constructor-company-list.component.html',
  styleUrl: './constructor-company-list.component.css'
})
export class ConstructorCompanyListComponent {

  
  constructor(
    private constructorCompanyService: ConstructorCompanyService
  ) {}
}
