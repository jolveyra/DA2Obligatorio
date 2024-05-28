import { Component, Input } from '@angular/core';
import { Flat } from '../flat.model';

@Component({
  selector: 'app-flat-item',
  templateUrl: './flat-item.component.html',
  styleUrl: './flat-item.component.css'
})
export class FlatItemComponent {
  @Input() flat: Flat = new Flat('', 0, 0, '', '', '', 0, 0, false);


}
