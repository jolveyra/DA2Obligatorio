import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ConstructorCompany } from '../../shared/constructor-company.model';

@Component({
  selector: 'app-constructor-company-item',
  templateUrl: './constructor-company-item.component.html',
  styleUrl: './constructor-company-item.component.css'
})
export class ConstructorCompanyItemComponent {

  @Input() constructorCompany: ConstructorCompany = new ConstructorCompany('', '');
  @Output() constructorCompanyToAssign: EventEmitter<string> = new EventEmitter<string>();

  onSelect(id: string): void {
    this.constructorCompanyToAssign.emit(id);
  }
}
