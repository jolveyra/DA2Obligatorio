import { Component } from '@angular/core';
import { ConstructorCompany } from '../../shared/constructor-company.model';
import { ConstructorCompanyService } from '../../services/constructor-company.service';
import { ConstructorCompanyAdministratorService } from '../../services/constructor-company-administrator.service';
import { Subscription } from 'rxjs';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-constructor-company-edit',
  templateUrl: './constructor-company-edit.component.html',
  styleUrl: './constructor-company-edit.component.css'
})
export class ConstructorCompanyEditComponent {

  isLoading: boolean = true;
  error: string = '';
  userId: string = '';
  userLoggedSub: Subscription = new Subscription();
  
  constructorCompany: ConstructorCompany = new ConstructorCompany('', '');
  constructorCompanyName: boolean = false;

  constructor(
    private constructorCompanyService: ConstructorCompanyService,
    private constructorCompanyAdministratorService: ConstructorCompanyAdministratorService,
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
  
  onEditConstructorCompany(): void {

    this.constructorCompanyService.updateConstructorCompany(this.constructorCompany)
    .subscribe(
      response => {
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