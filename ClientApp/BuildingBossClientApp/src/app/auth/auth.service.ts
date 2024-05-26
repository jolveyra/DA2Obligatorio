import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';

export interface AuthResponseData {
  // name: string; // FIXME: Add name to response from API so we show it up front
  token: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient, private router: Router) { }

  login(email: string, password: string) {
    return this.http.post<AuthResponseData>(
      'http://localhost:7122/api/v1/login', 
      {
        email,
        password
      }
    )
  }
}
