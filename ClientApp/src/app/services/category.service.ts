import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { environment } from './environment';

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
    return this.httpClient.get<CategoryResponseData[]>(`${environment.api}/api/v2/categories`)
      .pipe(
        map((response: CategoryResponseData[]) => response.map(category => ({
          id: category.id,
          name: category.name
        })))
      );
  }

  createCategory(name: string): Observable<CategoryResponseData> {
    return this.httpClient.post<CategoryResponseData>(`${environment.api}/api/v2/categories`,
      {
        name
      }
    );
  }
}
