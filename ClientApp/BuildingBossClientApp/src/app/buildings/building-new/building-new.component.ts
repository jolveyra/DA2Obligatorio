import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-building-new',
  templateUrl: './building-new.component.html',
  styleUrl: './building-new.component.css'
})
export class BuildingNewComponent {
  isLoading: boolean = false;
  error: string = '';
  
  onSubmit(form: NgForm): void {
    // FIXME: Implement this method
  }
}
