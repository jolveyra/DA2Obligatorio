import { Component, Input } from '@angular/core';
import { Building } from '../../../shared/building.model';

@Component({
  selector: 'app-building-item',
  templateUrl: './building-item.component.html',
  styleUrl: './building-item.component.css'
})
export class BuildingItemComponent {
  @Input() building: Building = new Building('', '', 0, 0, '', 0, '', '', '', 0, 0);
  @Input() role: string = '';
}
