import { Injectable } from '@angular/core';
import { Report } from '../reports/report.model';
import { Observable, map } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from './environment';

interface ReportData {
  filterName: string,
  pendingRequests: number,
  inProgressRequests: number,
  completedRequests: number,
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
    return this.httpClient.get<ReportData[]>(`${environment.api}/api/v2/reports?filter=${filter}`)
      .pipe(
        map((response: ReportData[]) => response.map(report => {
          const reportElem = new Report( 
          report.filterName,
          report.pendingRequests,
          report.inProgressRequests,
          report.completedRequests,
          report.averageCompletionTime);
          return reportElem;
      }))
      );
  }
}
