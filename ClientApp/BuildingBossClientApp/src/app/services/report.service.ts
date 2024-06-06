import { Injectable } from '@angular/core';
import { Report } from '../reports/report.model';
import { Observable, map } from 'rxjs';
import { HttpClient } from '@angular/common/http';

interface ReportData {
  filterName: string,
  pending: number,
  inProgress: number,
  completed: number,
  averageCompletionTime: number
}

@Injectable({
  providedIn: 'root'
})
export class ReportsService {

  constructor(
    private httpClient: HttpClient
  ) { }

  fetchReports(filter: string): Observable<Report[]> {
    return this.httpClient.get<ReportData[]>(`https://localhost:7122/api/v2/reports?filter=${filter}`)
      .pipe(
        map((response: ReportData[]) => response.map(report => {
          const reportElem = new Report( // FIXME: Al crear los valores pierde los 3 del medio, no entiendo por que, se los trate de asignar despues de la creacion y tambien, tampoco funcionan si los pongo como string
          report.filterName,
          report.pending,
          report.inProgress,
          report.completed,
          report.averageCompletionTime);
          return reportElem;
      }))
      );
  }
}
