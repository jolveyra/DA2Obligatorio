import { Component, Input } from '@angular/core';
import { User } from '../../../shared/user.model';

@Component({
  selector: 'app-employee-item',
  templateUrl: './employee-item.component.html',
  styleUrl: './employee-item.component.css'
})
export class EmployeeItemComponent {
  @Input() maintenanceEmployee: User = new User('', '', '', '');
}
