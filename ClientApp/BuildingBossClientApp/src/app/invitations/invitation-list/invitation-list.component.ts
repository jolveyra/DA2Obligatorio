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

  constructor(
    private invitationService: InvitationService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.invitations = this.invitationService.getInvitations(); // FIXME: Add a loading spinner for when it's fetching the administrators
  }

  onNewInvitation(): void {
    this.router.navigate(['new'], {relativeTo: this.route});
  }
}
