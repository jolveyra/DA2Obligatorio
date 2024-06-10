import { Injectable } from '@angular/core';
import { User } from '../shared/user.model';
import { Observable, map } from 'rxjs';
import { UserResponseData } from './userResponseData.model';
import { HttpClient } from '@angular/common/http';
import { environment } from './environment';

@Injectable({
  providedIn: 'root'
})
export class AdministratorService {
  
  constructor(
    private httpClient: HttpClient
  ) { }

  fetchAdministrators(): Observable<User[]> {
    return this.httpClient.get<UserResponseData[]>(`${environment.api}/api/v2/administrators`)
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
    return this.httpClient.post<UserResponseData>(`${environment}/api/v2/administrators`,
      {
        name: administrator.name,
        surname: administrator.surname,
        email: administrator.email,
        password
      }
    )
  }
}
