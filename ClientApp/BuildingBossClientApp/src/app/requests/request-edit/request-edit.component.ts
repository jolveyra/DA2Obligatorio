import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { ManagerRequest } from '../request.model';
import { RequestService } from '../../services/request.service';
import { Flat } from '../../shared/flat.model';
import { Building } from '../../shared/building.model';
import { User } from '../../shared/user.model';

@Component({
  selector: 'app-request-edit',
  templateUrl: './request-edit.component.html',
  styleUrl: './request-edit.component.css'
})
export class RequestEditComponent implements OnInit {
  request: ManagerRequest = new ManagerRequest('', '', new Flat('', '', 0, 0, '', '', '', 0, 0, false), new Building('', '', 0, 0, '', 0, '', '', '', 0, 0), '', new User('', '', '', ''), '');
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
    this.requestService.fetchManagerRequest(this.route.snapshot.params['id'])
      .subscribe(
        response => {
          
        }
      );
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
