import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map, tap } from 'rxjs';
import { ConstructorCompanyAdministrator } from '../shared/constructor-company-administrator.model';

interface ConstructorCompanyAdministratorResponseData {
  "id": string,
  "name": string,
  "surname": string,
  "email": string,
  "constructorCompanyId": string,
  "constructorCompanyName": string
}

@Injectable({
  providedIn: 'root'
})
export class ConstructorCompanyAdministratorService {

  constructor(
    private httpClient: HttpClient
  ) 
  { }

  fetchConstructorCompanyAdministrators(): Observable<ConstructorCompanyAdministratorResponseData[]> {
    return this.httpClient.get<ConstructorCompanyAdministratorResponseData[]>(`https://localhost:7122/api/v2/constructorCompanyAdministrators`)
      .pipe(
        map((response: ConstructorCompanyAdministratorResponseData[]) => response.map(admin => new ConstructorCompanyAdministrator(
          admin.id,
          admin.name,
          admin.surname,
          admin.email,
          admin.constructorCompanyId,
          admin.constructorCompanyName
        )))
      );
  }

  fetchConstructorCompanyAdministrator(id: string): Observable<ConstructorCompanyAdministratorResponseData> {
    return this.httpClient.get<ConstructorCompanyAdministratorResponseData>(`https://localhost:7122/api/v2/constructorCompanyAdministrators/${id}`)
      .pipe(
        tap( response => console.log(response)),
        map((response: ConstructorCompanyAdministratorResponseData) => new ConstructorCompanyAdministrator(
          response.id,
          response.name,
          response.surname,
          response.email,
          response.constructorCompanyId,
          response.constructorCompanyName
        ))
      );
    }
}