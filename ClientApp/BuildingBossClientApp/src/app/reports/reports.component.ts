import { Component } from '@angular/core';

import { Report } from './report.model';
import { ReportsService } from '../services/report.service';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrl: './reports.component.css'
})
export class ReportsComponent {
  isLoading: boolean = false;
  error: string = '';
  showBuildingReport: boolean = false;
  showEmployeeReport: boolean = false;
  buildingReports: Report[] = [];
  employeeReports: Report[] = [];

  constructor(
    private reportsService: ReportsService
  ) { }

  onBuildingReport(): void {
    this.showEmployeeReport = false;
    this.showBuildingReport = true;
    this.buildingReports = this.reportsService.getBuildingReports();
  }

  onEmployeeReport(): void {
    this.showBuildingReport = false;
    this.showEmployeeReport = true;
    this.employeeReports = this.reportsService.getEmployeeReports();
  }
}
