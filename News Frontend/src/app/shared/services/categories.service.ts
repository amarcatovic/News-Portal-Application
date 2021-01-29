import { CategoryNews } from './../models/CategoryNews';
import { Category } from './../models/Category';
import { ConstantsService } from './constants.service';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CategoriesService {

constructor(private http: HttpClient,
  private constants: ConstantsService,) { }

  /**
   * Returns Observable of Category array
   */
  public getCategories(): Observable<Category[]>{
    return this.http.get<Category[]>(`${this.constants.getApiUrl()}/api/categories`);
  }

  /**
   * Returns Observable of Category with all its news
   * @param id - category id
   */
  public getCategoryWithNews(id: number): Observable<CategoryNews>{
    return this.http.get<CategoryNews>(`${this.constants.getApiUrl()}/api/categories/${id}/news`);
  }
}
