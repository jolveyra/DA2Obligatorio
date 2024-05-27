import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { UserLogged } from './userLogged.model';

export interface AuthResponseData {
  name: string;
  token: string;
  role: string; 
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  userLogged = new BehaviorSubject<UserLogged>(new UserLogged('', '', ''));


  constructor(private http: HttpClient, private router: Router) { }

  login(email: string, password: string): Observable<AuthResponseData> {
    return this.http.post<AuthResponseData>(
      'http://localhost:5042/api/v1/login', 
      {
        email,
        password
      }
    )
  }

  logout(): void {
    this.userLogged.next(new UserLogged('', '', ''));
    this.router.navigate([''])
  }
}
