import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { AuthService } from './auth.service';
import { BuildingResponseData, FlatResponseData } from './building.service';
import { UserResponseData } from './userResponseData.model';

interface ManagerRequestResponseData {
  id: string;
  description: string;
  flat: FlatResponseData;
  building: BuildingResponseData,
  categoryName: string;
  assignedEmployee: UserResponseData;
  status: string;
}

interface EmployeeRequestResponseData {
  id: string;
  description: string;
  flat: FlatResponseData;
  building: BuildingResponseData,
  categoryName: string;
  status: string;
}

@Injectable({
  providedIn: 'root'
})
export class RequestService {

  constructor(
    private authService: AuthService,
    private httpClient: HttpClient
  ) { }

  fetchManagerRequests(): Observable<ManagerRequestResponseData[]> {
    return this.httpClient.get<ManagerRequestResponseData[]>('https://localhost:7122/api/v2/requests')
      .pipe(
        map((response: ManagerRequestResponseData[]) => response.map(request => {
          return {
            id: request.id,
            description: request.description,
            flat: request.flat,
            building: request.building,
            categoryName: request.categoryName,
            assignedEmployee: request.assignedEmployee,
            status: request.status
          };
        }))
      );
  }

  fetchMaintenanceEmployeeRequests(): Observable<EmployeeRequestResponseData[]> {
    return this.httpClient.get<EmployeeRequestResponseData[]>('https://localhost:7122/api/v2/employeeRequests')
      .pipe(
        map((response: EmployeeRequestResponseData[]) => response.map(request => {
          return {
            id: request.id,
            description: request.description,
            flat: request.flat,
            building: request.building,
            categoryName: request.categoryName,
            status: request.status
          };
        }))
      );
  }

  fetchManagerRequest(id: string): Observable<ManagerRequestResponseData> {
    return this.httpClient.get<ManagerRequestResponseData>(`https://localhost:7122/api/v2/requests/${id}`)
      .pipe(
        map((response: ManagerRequestResponseData) => {
          return {
            id: response.id,
            description: response.description,
            flat: response.flat,
            building: response.building,
            categoryName: response.categoryName,
            assignedEmployee: response.assignedEmployee,
            status: response.status
          };
        })
      );
  }

  updateRequestStatus(id: string, status: string): Observable<EmployeeRequestResponseData> {
    return this.httpClient.put<EmployeeRequestResponseData>(`https://localhost:7122/api/v2/employeeRequests/${id}`, { status })
      .pipe(
        map((response: EmployeeRequestResponseData) => {
          return {
            id: response.id,
            description: response.description,
            flat: response.flat,
            building: response.building,
            categoryName: response.categoryName,
            status: response.status
          }
        })
      );
  }
}
