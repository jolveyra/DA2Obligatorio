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
  buildingReports: Report[] = [];
  employeeReports: Report[] = [];

  constructor(
    private reportsService: ReportsService,
    private route: ActivatedRoute
  ) { }

  onReport(): void {
    this.error = '';
    let filter = this.route.snapshot.queryParams['filter'];
    this.showEmployeeReport = false;
    this.showBuildingReport = true;
    this.reportsService.fetchReports(filter).subscribe(
      reports => {
        console.log(reports); // FIXME: fix why pending to complete arent showing, just undefined, remove line when fixed
        this.buildingReports = reports;
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
