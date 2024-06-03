import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-request-new',
  templateUrl: './request-new.component.html',
  styleUrl: './request-new.component.css'
})
export class RequestNewComponent implements OnInit {
  isLoading: boolean = false;
  error: string = '';
  categories = ['Category 1', 'Category 2', 'Category 3'];
  selectedCategory: string = '';
  flats = ['Flat 1', 'Flat 2', 'Flat 3'];
  selectedFlat: string = '';
  employees = ['Employee 1', 'Employee 2', 'Employee 3'];
  selectedEmployee: string = '';

  constructor(
    
  ) { }

  ngOnInit(): void {
    // get flats by flat Id
  }

  selectCategory(category: string) {
      this.selectedCategory = category;
  }

  selectFlat(flat: string) {
      this.selectedFlat = flat;
      // FIXME: Implement call to get employees for selected building
  }

  selectEmployee(employee: string) {
      this.selectedCategory = employee;
  }

  onSubmit(form: NgForm): void {
    // FIXME: Implement this method
  }
}
