import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ImporterBuildingsService {

  constructor(
    private httpClient: HttpClient
  ) { }
  
  importBuildings(importerName: string, fileName: string): Observable<string> {
    return this.httpClient.post<string>('https://localhost:7122/api/v2/importers',
      {
        importerName,
        fileName
      }
    )
    .pipe(
      map((response: string) => response)
    );
  }

}
