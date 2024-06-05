import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';

interface TeapotResponseData {
  message: string;
}

@Injectable({
  providedIn: 'root'
})
export class TeapotService {

  constructor(
    private httpClient: HttpClient
  ) { }

  getTeapot(): Observable<TeapotResponseData> {
    return this.httpClient.get<TeapotResponseData>('https://localhost:7122/api/v2/teapot')
      .pipe(
        map((response: TeapotResponseData) => {
          return {
            message: response.message
          };
        })
      );
  }
}
