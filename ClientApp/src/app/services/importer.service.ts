import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { environment } from './environment';

interface ImporterResponseData {
  id: string;
  name: string;
}

@Injectable({
  providedIn: 'root'
})
export class ImporterService {

  constructor(
    private httpClient: HttpClient
  ) { }

  fetchImporters(): Observable<ImporterResponseData[]> {
    return this.httpClient.get<ImporterResponseData[]>(`${environment.api}/api/v2/importers`)
      .pipe(
        map((response: ImporterResponseData[]) => response.map(importer => ({
          id: importer.id,
          name: importer.name
        })))
      );
  }

  createImporter(name: string): Observable<ImporterResponseData> {
    return this.httpClient.post<ImporterResponseData>(`${environment.api}/api/v2/importers`,
      {
        name
      }
    ).pipe(
      map((response: ImporterResponseData) => ({
        id: response.id,
        name: response.name
      }))
    );
  }
}
