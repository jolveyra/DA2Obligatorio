import { Component, Input } from '@angular/core';
import { ConstructorCompanyAdministrator } from '../../shared/constructor-company-administrator.model';

@Component({
  selector: 'app-ccadministrator-item',
  templateUrl: './ccadministrator-item.component.html',
  styleUrl: './ccadministrator-item.component.css'
})
export class CcadministratorItemComponent {
  @Input() ccAdministrator: ConstructorCompanyAdministrator = new ConstructorCompanyAdministrator('', '', '', '', '', '');
}
