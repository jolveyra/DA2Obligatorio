import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Invitation } from '../invitation.model';
import { InvitationService } from '../../services/invitation.service';

@Component({
  selector: 'app-invitation-list',
  templateUrl: './invitation-list.component.html',
  styleUrl: './invitation-list.component.css'
})
export class InvitationListComponent {
  invitations: Invitation[] = [];
  error: string = '';

  constructor(
    private invitationService: InvitationService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.invitationService.fetchInvitations()
      .subscribe(
        response => {
          this.invitations = response;
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

  onNewInvitation(): void {
    this.router.navigate(['new'], {relativeTo: this.route});
  }

  onDeleteInvitation(id: string): void {
    this.invitationService.deleteInvitation(id)
      .subscribe(
        () => {
          this.invitations = this.invitations.filter(invitation => invitation.id !== id);
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
