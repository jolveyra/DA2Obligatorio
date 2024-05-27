import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import { UserLogged } from './userLogged.model';

export interface AuthResponseData {
  // name: string; // FIXME: Add name to response from API so we show it up front
  token: string;
  // role: string; // FIXME: Add role to response from API so we can choose what to show
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  user = new Subject<UserLogged>();

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
}
