import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Invitation } from '../../invitation.model';

@Component({
  selector: 'app-invitation-item',
  templateUrl: './invitation-item.component.html',
  styleUrl: './invitation-item.component.css'
})
export class InvitationItemComponent {
  @Input() invite: Invitation = new Invitation('', '', '', new Date(), '', false, false);
  today: Date = new Date();
  @Output() invitationToDelete: EventEmitter<string> = new EventEmitter<string>();

  onDelete(id: string): void {
    this.invitationToDelete.emit(id);
  }

  getStatusClass(): string {
    if (this.invite.isAnswered) {
      return this.invite.isAccepted ? 'text-success' : 'text-danger';
    } else {
      return this.invite.expirationDate < this.today ? 'text-danger' : 'text-primary';
    }
  }
}
