import { Injectable } from '@angular/core';
import { User } from '../shared/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserSettingsService {
  user: User = new User('id1', 'nombre', 'apellido', 'contraseña'); // FIXME: leave it blank when request done

  constructor() { }

  getUser(): User {
    return this.user;
  }

  updateUser(name: string, surname: string, password: string): void {
    // FIXME: change whats under this line and replace it with a request to the server wth these values in the body
    this.user.name = name;
    this.user.surname = surname;
    this.user.password = password;
  }
}
