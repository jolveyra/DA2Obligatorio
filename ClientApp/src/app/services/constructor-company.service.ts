import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { ConstructorCompany } from '../shared/constructor-company.model';
import { environment } from './environment';

interface ConstructorCompanyResponseData {
  id: string;
  name: string;
}

@Injectable({
  providedIn: 'root'
})
export class ConstructorCompanyService {

  constructor(
    private httpClient: HttpClient
  ) { }

  createConstructorCompany(constructorCompany: ConstructorCompany): Observable<ConstructorCompanyResponseData> {
    return this.httpClient.post<ConstructorCompanyResponseData>(`${environment.api}/api/v2/constructorCompanies`,
      {
        id: constructorCompany.id,
        name: constructorCompany.name
      }
    )
      .pipe(
        map((response: ConstructorCompanyResponseData) => new ConstructorCompany(
          response.id,
          response.name))
      );
  }

  fetchConstructorCompany(id: string): Observable<ConstructorCompany> {
    return this.httpClient.get<ConstructorCompanyResponseData>(`${environment.api}/api/v2/constructorCompanies/${id}`)
      .pipe(
        map((response: ConstructorCompanyResponseData) => new ConstructorCompany(
          response.id,
          response.name))
      );
  }

  fetchConstructorCompanies(): Observable<ConstructorCompany[]> {
    return this.httpClient.get<ConstructorCompanyResponseData[]>(`${environment.api}/api/v2/constructorCompanies`)
      .pipe(
        map((response: ConstructorCompanyResponseData[]) => response.map(constructorCompany => new ConstructorCompany(
          constructorCompany.id,
          constructorCompany.name)))
      );
  }

  updateConstructorCompany(constructorCompany: ConstructorCompany): Observable<ConstructorCompanyResponseData> {
    return this.httpClient.put<ConstructorCompanyResponseData>(`${environment.api}/api/v2/constructorCompanies/${constructorCompany.id}`,
      {
        name: constructorCompany.name
      }
    )
      .pipe(
        map((response: ConstructorCompanyResponseData) => new ConstructorCompany(
          response.id,
          response.name))
      );
  }
}