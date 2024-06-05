import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { ConstructorCompany } from '../shared/constructor-company.model';

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
    return this.httpClient.post<ConstructorCompanyResponseData>(`https://localhost:7122/api/v2/constructorCompanies`,
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
    return this.httpClient.get<ConstructorCompanyResponseData>(`https://localhost:7122/api/v2/constructorCompanies/${id}`)
      .pipe(
        map((response: ConstructorCompanyResponseData) => new ConstructorCompany(
          response.id,
          response.name))
      );
  }
}
