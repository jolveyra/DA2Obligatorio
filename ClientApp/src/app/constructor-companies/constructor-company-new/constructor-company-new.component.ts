import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { ConstructorCompany } from '../../shared/constructor-company.model';
import { ConstructorCompanyService } from '../../services/constructor-company.service';

@Component({
  selector: 'app-constructor-company-new',
  templateUrl: './constructor-company-new.component.html',
  styleUrl: './constructor-company-new.component.css'
})
export class ConstructorCompanyNewComponent {
  isLoading: boolean = false;
  error: string = '';
  toCreateConstructorCompany = new ConstructorCompany('', '');

  constructor(
    private constructorCompanyService: ConstructorCompanyService,
    private router: Router,
    private route: ActivatedRoute
  ) {}
  
  onSubmit(form: NgForm): void {
    if (!form.valid) {
      return;
    }

    this.toCreateConstructorCompany.name = form.value.name;

    this.isLoading = true;
    this.error = '';

    this.constructorCompanyService.createConstructorCompany(this.toCreateConstructorCompany).subscribe(
      response => {
        this.isLoading = false;
        this.router.navigate([''], { relativeTo: this.route });
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
  
  onBackIcon(): void {
    this.router.navigate(['../'], { relativeTo: this.route });
  }
}
