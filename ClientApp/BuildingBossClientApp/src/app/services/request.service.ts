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
      )
      
  }
  // return this.httpClient.get<RequestResponseData[]>('https://localhost:7122/api/v2/employeeRequests');
}
