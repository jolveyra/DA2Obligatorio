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
  buildingReports: Report[] = [
    new Report('Bella vista', 3, 5, 3, 4.5),
    new Report('Mirador', 12, 0, 4, 5),
    new Report('Casita', 10, 2, 18, 4),
    new Report('Miedi ficio', 30, 1, 2, 3.7),
  ];
  employeeReports: Report[] = [
    new Report('Jujurio Moperei', 3, 5, 3, 4.5),
    new Report('Jijona Ningunea', 12, 0, 4, 5),
    new Report('Cusuchi Pipulona', 10, 2, 18, 4),
    new Report('Goncho Mamisterio', 30, 1, 2, 3.7),
  ];

  constructor(
    private httpClient: HttpClient
  ) { }

  fetchReports(filter: string): Observable<Report[]> {
    return this.httpClient.get<ReportData[]>(`https://localhost:7122/api/v1/reports?filter=${filter}`)
      .pipe(
        map((response: ReportData[]) => response.map(report => new Report(
          report.filterName,
          report.pending,
          report.inProgress,
          report.completed,
          report.averageCompletionTime
        )))
      );
  }
}
