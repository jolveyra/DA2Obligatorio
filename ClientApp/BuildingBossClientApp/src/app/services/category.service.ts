import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';

interface CategoryResponseData {
  id: string;
  name: string;
}

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(
    private httpClient: HttpClient
  ) { }

  fetchCategories(): Observable<CategoryResponseData[]> {
    return this.httpClient.get<CategoryResponseData[]>('https://localhost:7122/api/v2/categories')
      .pipe(
        map((response: CategoryResponseData[]) => response.map(category => ({
          id: category.id,
          name: category.name
        })))
      );
  }
}
