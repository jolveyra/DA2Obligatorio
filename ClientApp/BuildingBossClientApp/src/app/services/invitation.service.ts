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

  constructor(
    private httpClient: HttpClient
  ) { }

  fetchInvitations(): Observable<InvitationResponseData[]> {
    return this.httpClient.get<InvitationResponseData[]>('https://localhost:7122/api/v2/invitations')
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
    return this.httpClient.post<InvitationResponseData>('https://localhost:7122/api/v2/invitations',
      {
        name: invitation.name,
        email: invitation.email,
        daysToExpiration: expDays,
        role: invitation.role
      }
    )
  }

  // TODO: implement updateInvitation method
  
  deleteInvitation(id: string) {
    return this.httpClient.delete(`https://localhost:7122/api/v2/invitations/${id}`);
  }
}