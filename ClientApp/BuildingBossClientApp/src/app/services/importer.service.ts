import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';

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
    return this.httpClient.get<ImporterResponseData[]>('https://localhost:7122/api/v2/importers')
      .pipe(
        map((response: ImporterResponseData[]) => response.map(importer => ({
          id: importer.id,
          name: importer.name
        })))
      );
  }

  createImporter(name: string): Observable<ImporterResponseData> {
    return this.httpClient.post<ImporterResponseData>('https://localhost:7122/api/v2/importers',
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
