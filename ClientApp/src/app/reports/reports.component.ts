import { Component } from '@angular/core';

import { Report } from './report.model';
import { ReportsService } from '../services/report.service';
import { ActivatedRoute, NavigationExtras, Router } from '@angular/router';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrl: './reports.component.css'
})
export class ReportsComponent {
  error: string = '';
  showBuildingReport: boolean = false;
  showEmployeeReport: boolean = false;
  reports: Report[] = [];
  noReports: boolean = false;

  constructor(
    private reportsService: ReportsService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  onReport(filter: string): void {
    this.error = '';
    const newParams = { filter: filter };
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: newParams,
      queryParamsHandling: 'merge'
    });
    this.reportsService.fetchReports(filter).subscribe(
      reports => {
        this.reports = reports;
        
        if (this.reports.length === 0) {
          this.noReports = true;
        } else if (filter === 'employee') {
          this.showEmployeeReport = true;
          this.showBuildingReport = false;
        } else if (filter === 'building') {
          this.showEmployeeReport = false;
          this.showBuildingReport = true;
        }
      },
      error => {
        let errorMessage = "An unexpected error has occured, please retry later."
        if (error.error && error.error.errorMessage) {
          this.error = error.error.errorMessage;
        } else {
          this.error = errorMessage;
        }
      }
    );
  }
}
