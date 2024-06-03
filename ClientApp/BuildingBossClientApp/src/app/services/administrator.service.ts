import { Injectable } from '@angular/core';
import { User } from '../shared/user.model';
import { Observable, map } from 'rxjs';
import { UserResponseData } from './userResponseData.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AdministratorService {
  private administrators: User[] = [];

  constructor(
    private httpClient: HttpClient
  ) { }

  fetchAdministrators(): Observable<User[]> {
    return this.httpClient.get<UserResponseData[]>('https://localhost:7122/api/v2/administrators')
      .pipe(
        map((response: UserResponseData[]) => response.map(administrator => new User(
          administrator.id,
          administrator.name,
          administrator.surname,
          administrator.email
        )))
      );
  }

  createAdministrator(administrator: User, password: string) {
    return this.httpClient.post<UserResponseData>('https://localhost:7122/api/v2/administrators',
      {
        name: administrator.name,
        surname: administrator.surname,
        email: administrator.email,
        password
      }
    )
  }
}
