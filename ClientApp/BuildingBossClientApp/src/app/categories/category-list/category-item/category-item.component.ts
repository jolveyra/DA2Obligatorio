import { Component, Input } from '@angular/core';
import { Category } from '../../../shared/category.model';

@Component({
  selector: 'app-category-item',
  templateUrl: './category-item.component.html',
  styleUrl: './category-item.component.css'
})
export class CategoryItemComponent {
  @Input() category: Category = new Category('', '');
}
