import { Component, Input } from '@angular/core';
import { User } from '../../../shared/user.model';

@Component({
  selector: 'app-administrator-item',
  templateUrl: './administrator-item.component.html',
  styleUrl: './administrator-item.component.css'
})
export class AdministratorItemComponent {
  @Input() administrator: User = new User('', '', '', '');
}
