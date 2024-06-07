import { Component, Input } from '@angular/core';
import { Importer } from '../../../shared/importer.model';

@Component({
  selector: 'app-import-item',
  templateUrl: './import-item.component.html',
  styleUrl: './import-item.component.css'
})
export class ImportItemComponent {
  @Input() importer: Importer = new Importer('');
}
