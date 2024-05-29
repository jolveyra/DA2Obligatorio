import { Injectable } from '@angular/core';
import { Invitation } from './invitation.model';

@Injectable({
  providedIn: 'root'
})
export class InvitationService {
  private invitations: Invitation[] = [ // FIXME: leave empty and make a request for real data
    new Invitation('1', 'John', 'john@mail.com', new Date(2024, 8), 'Manager', false, false),
    new Invitation('1', 'Jean', 'jeann@mail.com', new Date(2024, 8), 'Manager', true, true),
    new Invitation('1', 'Jack', 'jack@mail.com', new Date(2024, 8), 'Manager', false, true),
    new Invitation('1', 'Javier', 'javier@mail.com', new Date(2024, 8), 'Constructor company administrator', true, false), // To check only becouse this never should happen
    new Invitation('1', 'Jose', 'jose@mail.com', new Date(2024, 3), 'Constructor company administrator', false, true),
    new Invitation('1', 'Juan', 'juan@mail.com', new Date(2024, 2), 'Constructor company administrator', false, false),
  ];

  constructor() { }

  getInvitations(): Invitation[] {
    return this.invitations.slice();
  }
}