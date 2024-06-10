import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { UserLogged } from './userLogged.model';
import { environment } from './environment';

export interface AuthResponseData {
  id: string;
  name: string;
  token: string;
  role: string; 
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  userLogged = new BehaviorSubject<UserLogged>(new UserLogged('', '', '', ''));

  constructor(private http: HttpClient, private router: Router) { }

  login(email: string, password: string): Observable<AuthResponseData> {
    return this.http.post<AuthResponseData>(
      `${environment.api}/api/v2/login`,
      {
        email,
        password
      }
    ).pipe(
      tap(user => {
        this.userLogged.next(new UserLogged(user.id, user.name, user.token, user.role));
        localStorage.setItem('user', JSON.stringify(user));
      })
    );
  }

  autoLogin(): void {
    const userInStorage = localStorage.getItem('user');

    if (!userInStorage) {
      return;
    }

    const user: {
      id: string;
      name: string;
      token: string;
      role: string;
    } = JSON.parse(userInStorage);
    
    this.userLogged.next(new UserLogged(user.id, user.name, user.token, user.role));
  }

  getAuthToken(): string {
    return this.userLogged.value.token;
  }

  logout(): void {
    localStorage.removeItem('user');
    this.userLogged.next(new UserLogged('', '', '', ''));
    this.router.navigate([''])
  }
}