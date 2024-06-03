import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../shared/user.model';
import { Observable, map } from 'rxjs';
import { UserResponseData } from './userResponseData.model';

@Injectable({
  providedIn: 'root'
})
export class ManagerService {
  
  constructor(
    private httpClient: HttpClient
  ) { }

  fetchManagers(): Observable<User[]> {
    return this.httpClient.get<UserResponseData[]>('https://localhost:7122/api/v2/managers')
      .pipe(
        map((response: UserResponseData[]) => response.map(manager => new User(
          manager.id,
          manager.name,
          manager.surname,
          manager.email
        )))
      );
  }
}
