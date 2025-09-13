import { Component, Input } from '@angular/core';
import { User } from '../../../shared/user.model';

@Component({
  selector: 'app-manager-item',
  templateUrl: './manager-item.component.html',
  styleUrl: './manager-item.component.css'
})
export class ManagerItemComponent {
  @Input() manager: User = new User('', '', '', '');
}
