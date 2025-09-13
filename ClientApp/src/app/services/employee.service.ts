import { Injectable } from '@angular/core';
import { User } from '../shared/user.model';
import { Observable, map } from 'rxjs';
import { UserResponseData } from './userResponseData.model';
import { HttpClient } from '@angular/common/http';
import { environment } from './environment';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(
    private httpClient: HttpClient
  ) { }

  fetchMaintenanceEmployees(): Observable<User[]> {
    return this.httpClient.get<UserResponseData[]>(`${environment.api}/api/v2/maintenanceEmployees`)
      .pipe(
        map((response: UserResponseData[]) => response.map(employee => new User(
            employee.id,
            employee.name,
            employee.surname,
            employee.email
        )))
      );
  }

  createMaintenanceEmployee(employee: User, password: string): Observable<User> {
    return this.httpClient.post<UserResponseData>(`${environment.api}/api/v2/maintenanceEmployees`,
    {
      name: employee.name,
      surname: employee.surname,
      email: employee.email,
      password: password
    })
      .pipe(
        map((response: UserResponseData) => new User(
            response.id,
            response.name,
            response.surname,
            response.email
        ))
      );
  }
}
