import { Component, OnInit } from '@angular/core';
import { ConstructorCompanyAdministratorService } from '../services/constructor-company-administrator.service';
import { ConstructorCompanyAdministrator } from '../shared/constructor-company-administrator.model';

@Component({
  selector: 'app-ccadministrators',
  templateUrl: './ccadministrators.component.html',
  styleUrl: './ccadministrators.component.css'
})
export class CcadministratorsComponent implements OnInit {
  noAdmins: boolean = false;
  error: string = '';
  constructorCompanyAdministrators: ConstructorCompanyAdministrator[] = [];

  constructor(
    private constructorCompanyAdminService: ConstructorCompanyAdministratorService
  ) {}

  ngOnInit() {
    this.constructorCompanyAdminService.fetchConstructorCompanyAdministrators()
      .subscribe(
        response => {
          this.constructorCompanyAdministrators = response;
          this.noAdmins = this.constructorCompanyAdministrators.length === 0;
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
