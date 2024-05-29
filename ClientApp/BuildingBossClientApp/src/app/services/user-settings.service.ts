import { Injectable } from '@angular/core';
import { User } from '../shared/user.model';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';

interface UserResponseData {
  id: string;
  name: string;
  surname: string;
  email: string;
}

@Injectable({
  providedIn: 'root'
})
export class UserSettingsService {

  constructor(
    private httpClient: HttpClient
  ) { }

  fetchUser(): Observable<User> {
    return this.httpClient.get<UserResponseData>('https://localhost:7122/api/v1/userSettings')
      .pipe(
        map((response: UserResponseData) => new User(
          response.id,
          response.name,
          response.surname,
          response.email
        ))
      );
  }

  updateUser(name: string, surname: string, password: string): Observable<User> {
    return this.httpClient.put<UserResponseData>('https://localhost:7122/api/v1/userSettings', {
      name,
      surname,
      password
    }).pipe(
      map((response: UserResponseData) => new User(
        response.id,
        response.name,
        response.surname,
        response.email
      ))
    );
  }
}
