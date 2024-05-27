import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-invitation-new',
  templateUrl: './invitation-new.component.html',
  styleUrl: './invitation-new.component.css'
})
export class InvitationNewComponent {
  isLoading: boolean = false;
  error: string = '';
  roles = ['Manager', 'Constructor company administrator'];

  onSubmit(form: NgForm): void {

  }
}
