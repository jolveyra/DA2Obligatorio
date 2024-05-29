import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { RequestService } from '../request.service';
import { ActivatedRoute } from '@angular/router';
import { Request } from '../request.model';

@Component({
  selector: 'app-request-edit',
  templateUrl: './request-edit.component.html',
  styleUrl: './request-edit.component.css'
})
export class RequestEditComponent implements OnInit {
  request: Request = new Request('', '', '', '', '', '');
  isLoading: boolean = false;
  error: string = '';
  categories = ['Category 1', 'Category 2', 'Category 3'];
  selectedCategory: string = '';
  employees = ['Employee 1', 'Employee 2', 'Employee 3'];
  selectedEmployee: string = '';
  description: string = '';

  constructor(
    private requestService: RequestService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    let request: Request = this.requestService.getRequest(this.route.snapshot.params['id']);
    
    if (request.id !== '') {
      this.request = request;
      this.selectedCategory = this.request.categoryId;
      this.description = this.request.description;
      this.selectedEmployee = this.request.assignedEmployeeId;
    } else {
      this.error = 'Request not found';
    }

  }

  selectCategory(category: string) {
      this.selectedCategory = category;
  }

  selectEmployee(employee: string) {
      this.selectedCategory = employee;
  }

  onSubmit(form: NgForm): void {
    // FIXME: Implement this method
  }
}
