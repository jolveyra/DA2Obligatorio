import { Component } from '@angular/core';
import { CategoryService } from '../../services/category.service';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-category-new',
  templateUrl: './category-new.component.html',
  styleUrl: './category-new.component.css'
})
export class CategoryNewComponent {
  isLoading: boolean = false;
  error: string = '';

  constructor(
    private categoryService: CategoryService,
    private router: Router
  ) {}

  onSubmit(form: NgForm): void {
    if (!form.valid) {
      return;
    }
    
    this.isLoading = true;
    this.error = '';
    this.categoryService.createCategory(form.value.name)
      .subscribe(
        () => {
          this.isLoading = false;
          this.router.navigate(['/categories']);
        },
        error => {
          let errorMessage = "An unexpected error has occured, please retry later."
          if (error.error && error.error.errorMessage) {
            this.error = error.error.errorMessage;
          } else {
            this.error = errorMessage;
          }
          this.isLoading = false;
        }
      );
  }
}
