import { Component } from '@angular/core';
import { ConstructorCompany } from '../../shared/constructor-company.model';
import { ConstructorCompanyService } from '../../services/constructor-company.service';

@Component({
  selector: 'app-constructor-company-edit',
  templateUrl: './constructor-company-edit.component.html',
  styleUrl: './constructor-company-edit.component.css'
})
export class ConstructorCompanyEditComponent {

  isLoading: boolean = true;
  error: string = '';
  
  constructorCompany: ConstructorCompany = new ConstructorCompany('', '');
  constructorCompanyName: boolean = false;

  constructor(
    private constructorCompanyService: ConstructorCompanyService
  ) {}

  
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
