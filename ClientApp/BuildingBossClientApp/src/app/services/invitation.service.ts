import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable, map } from 'rxjs';

import { Invitation } from '../invitations/invitation.model';

interface InvitationResponseData {
  id: string,
  name: string,
  email: string,
  expirationDate: Date,
  role: string,
  isAccepted: boolean,
  isAnswered: boolean
}

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

  constructor(
    private httpClient: HttpClient
  ) { }

  fetchInvitations(): Observable<InvitationResponseData[]> {
    return this.httpClient.get<InvitationResponseData[]>('https://localhost:7122/api/v1/invitations')
      .pipe(
        map((response: InvitationResponseData[]) => response.map(invitation => new Invitation(
          invitation.id,
          invitation.name,
          invitation.email,
          new Date(invitation.expirationDate),
          invitation.role,
          invitation.isAccepted,
          invitation.isAnswered
        )))
      );
  }

  createInvitation(invitation: Invitation, expDays: number) {
    return this.httpClient.post<InvitationResponseData>('https://localhost:7122/api/v1/invitations',
      {
        name: invitation.name,
        email: invitation.email,
        daysToExpire: expDays,
        role: invitation.role
      }
    )
  }
}