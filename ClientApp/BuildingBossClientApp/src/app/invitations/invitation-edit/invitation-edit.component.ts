import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { InvitationService } from '../../services/invitation.service';
import { Invitation } from '../invitation.model';

@Component({
  selector: 'app-invitation-edit',
  templateUrl: './invitation-edit.component.html',
  styleUrl: './invitation-edit.component.css'
})
export class InvitationEditComponent implements OnInit {
  invitation: Invitation = new Invitation('', '', '', new Date(), '', false, false);
  error: string = '';
  answer: boolean = true;

  constructor(
    private invitationService: InvitationService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.invitationService.fetchInvitation(this.route.snapshot.params['id'])
      .subscribe(
        response => {
          this.invitation = response
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

  selectAnswer(answer: boolean) {
    this.answer = answer;
  }

  onSubmit() {
    this.invitationService.updateInvitationStatus(this.route.snapshot.params['id'], this.answer)
      .subscribe(
        response => {
          this.invitation = response;
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
