import { Component, input } from '@angular/core';
import { ConstructorCompany } from '../../shared/constructor-company.model';
import { ConstructorCompanyService } from '../../services/constructor-company.service';
import { ConstructorCompanyAdministratorService } from '../../services/constructor-company-administrator.service';
import { Subscription } from 'rxjs';
import { AuthService } from '../../services/auth.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-constructor-company-edit',
  templateUrl: './constructor-company-edit.component.html',
  styleUrl: './constructor-company-edit.component.css'
})
export class ConstructorCompanyEditComponent {

  isLoading: boolean = false;
  error: string = '';
  userId: string = '';
  userLoggedSub: Subscription = new Subscription();
  success: boolean = false;
  
  constructorCompany: ConstructorCompany = new ConstructorCompany('', '');
  constructorCompanyName: boolean = false;
  inputConstructorCompanyName: string = '';

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
          this.inputConstructorCompanyName = constructorCompanyAdministrator.constructorCompanyName;
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
  
  onSubmit(form: NgForm): void {
    if (!form.valid) {
      return;
    }

    this.isLoading = true;
    this.success = false;
    this.error = '';
    this.constructorCompany.name = this.inputConstructorCompanyName;
    this.constructorCompanyService.updateConstructorCompany(this.constructorCompany)
    .subscribe(
      response => {
        this.constructorCompany.name = response.name;
        this.constructorCompany.id = response.id;
        this.isLoading = false;
        this.success = true;
      },
      error => {
        let errorMessage = "An unexpected error has occured, please retry later."
        this.isLoading = false;
        if (error.error && error.error.errorMessage) {
          this.error = error.error.errorMessage;
        } else {
          this.error = errorMessage;
        }
      }
    );

  }
}