import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { InvitationService } from '../../services/invitation.service';
import { Invitation } from '../invitation.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-invitation-new',
  templateUrl: './invitation-new.component.html',
  styleUrl: './invitation-new.component.css'
})
export class InvitationNewComponent {
  isLoading: boolean = false;
  error: string = '';
  roles = ['Manager', 'Constructor company administrator'];

  constructor(
    private invitationService: InvitationService,
    private router: Router
  ) {}

  onSubmit(form: NgForm): void {
    if (!form.valid) {
      return;
    }

    this.isLoading = true;

    const newInvitation = new Invitation(
      '',
      form.value.name,
      form.value.email,
      new Date(),
      form.value.role === 'Manager' ? 'Manager' : 'ConstructorCompanyAdmin',
      false,
      false
    );

    this.invitationService.createInvitation(newInvitation, form.value.expDays)
      .subscribe(
        response => {
          this.isLoading = false;
          this.router.navigate(['/invitations']);
        },
        error => {
          let errorMessage = "An unexpected error has occured, please retry later."
          if (error.error && error.error.errorMessage) {
            this.error = error.error.errorMessage;
          } else {
            this.error = errorMessage;
          }
          this.isLoading = false; // TODO: check that in all forms Ive put this
        }
      );
  }
}
