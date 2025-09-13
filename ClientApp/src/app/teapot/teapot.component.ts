import { Component, OnInit } from '@angular/core';
import { TeapotService } from '../services/teapot.service';

@Component({
  selector: 'app-teapot',
  templateUrl: './teapot.component.html',
  styleUrl: './teapot.component.css'
})
export class TeapotComponent implements OnInit {
  error: string = '';
  teapotMessage: string = '';

  constructor(
    private teapotService: TeapotService
  ) {}

  ngOnInit(): void {
    this.teapotService.getTeapot().subscribe(
      response => {},
      error => {
        if (error.status === 418) {
          this.teapotMessage = error.error;
        } else {
          let errorMessage = "An unexpected error has occured, please retry later."
          if (error.error && error.error.errorMessage) {
            this.error = error.error.errorMessage;
          } else {
            this.error = errorMessage;
          }
        }
      }
    );
  }
}
