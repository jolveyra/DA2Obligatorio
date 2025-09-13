import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable, map } from 'rxjs';

import { Invitation } from '../invitations/invitation.model';
import { environment } from './environment';

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
    return this.httpClient.get<InvitationResponseData[]>(`${environment.api}/api/v2/invitations`)
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

  fetchInvitation(id: string): Observable<InvitationResponseData> {
    return this.httpClient.get<InvitationResponseData>(`${environment.api}/api/v2/invitations/${id}`)
      .pipe(
        map((invitation: InvitationResponseData) => new Invitation(
          invitation.id,
          invitation.name,
          invitation.email,
          new Date(invitation.expirationDate),
          invitation.role,
          invitation.isAccepted,
          invitation.isAnswered
        ))
      );
  }

  createInvitation(invitation: Invitation, expDays: number): Observable<InvitationResponseData> {
    return this.httpClient.post<InvitationResponseData>(`${environment.api}/api/v2/invitations`,
      {
        name: invitation.name,
        email: invitation.email,
        daysToExpiration: expDays,
        role: invitation.role
      }
    )
  }

  updateInvitationStatus(id: string, answer: boolean): Observable<InvitationResponseData> {
    return this.httpClient.put<InvitationResponseData>(`${environment.api}/api/v2/invitations/${id}`,
      {
        isAccepted: answer
      }
    );
  }
  
  deleteInvitation(id: string): Observable<void> {
    return this.httpClient.delete<void>(`${environment.api}/api/v2/invitations/${id}`);
  }
}