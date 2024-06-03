import { Component, Input } from '@angular/core';
import { Invitation } from '../../invitation.model';

@Component({
  selector: 'app-invitation-item',
  templateUrl: './invitation-item.component.html',
  styleUrl: './invitation-item.component.css'
})
export class InvitationItemComponent {
  @Input() invite: Invitation = new Invitation('', '', '', new Date(), '', false, false);
  today: Date = new Date();

  onDelete(id: string): void {}
}
