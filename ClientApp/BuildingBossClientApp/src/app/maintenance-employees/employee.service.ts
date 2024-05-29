import { Injectable } from '@angular/core';
import { User } from '../shared/user.model';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  private maintenanceEmployees: User[] = [ // FIXME: leave empty and make a request for real data
    new User('4321431', 'Joaquin', 'Garcia', 'joaco@gmail.com'),
    new User('143214321', 'Juan', 'Gaboto', 'juan@gmail.com'),
    new User('43214123', 'Javier', 'Gomez', 'javier@gmail.com'),
    new User('431432143214', 'Jose', 'Gonzalez', 'jose@gmail.com')
  ];

  constructor() { }

  getMaintenanceEmployees(): User[] {
    return this.maintenanceEmployees.slice();
  }
}
