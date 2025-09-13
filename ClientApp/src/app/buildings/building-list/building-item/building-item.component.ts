import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Building } from '../../../shared/building.model';
import { BuildingFlats } from '../../../shared/buildingFlats.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-building-item',
  templateUrl: './building-item.component.html',
  styleUrl: './building-item.component.css'
})
export class BuildingItemComponent {
  @Input() building: Building | BuildingFlats = new BuildingFlats('', '', 0, [], '', 0, '', '', '', 0, 0, []);
  @Input() role: string = '';
  @Input() manager: string = '';
  @Output() buildingDeleted: EventEmitter<string> = new EventEmitter<string>();

  constructor(
    private router: Router
  ) { }

  onDelete() {
    this.buildingDeleted.emit(this.building.id);
  }

  onAdminEdit() {
    this.router.navigate([`/buildings/${this.building.id}/edit`]);
  }

  onManagerEdit() {
    this.router.navigate([`/buildings/${this.building.id}`]);
  }
}
