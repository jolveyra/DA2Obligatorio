import { Injectable } from '@angular/core';
import { Request } from '../requests/request.model';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { AuthService } from './auth.service';

export interface RequestResponseData {
  id: string;
  description: string;
  flatId: string;
  buildingId: string,
  categoryName: string;
  assignedEmployeeId: string;
  status: string;
  startingDate: string;
  completionDate: string;
}

@Injectable({
  providedIn: 'root'
})
export class RequestService {
  private requests: Request[] = [];

  constructor(
    private authService: AuthService,
    private httpClient: HttpClient
  ) { }

  fetchRequests(): Observable<RequestResponseData[]> {
    let userRole = this.authService.userLogged.value.role;
    if (userRole === 'MaintenanceEmployee') {
      return this.httpClient.get<RequestResponseData[]>('https://localhost:7122/api/v2/employeeRequests');
    } else {
      return this.httpClient.get<RequestResponseData[]>('https://localhost:7122/api/v2/requests');
    }
  }
}
