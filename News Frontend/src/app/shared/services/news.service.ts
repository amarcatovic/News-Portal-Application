import { CookieService } from 'ngx-cookie-service';
import { NewsAddEdit } from './../models/NewsAddEdit';
import { News } from './../models/News';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ConstantsService } from './constants.service';

@Injectable({
  providedIn: 'root'
})
export class NewsService {

private news: News = null;

constructor(private http: HttpClient,
  private constants: ConstantsService,) { }

  /**
   * Returns Observable of News array
   */
  public getAllNews(): Observable<News[]>{
    return this.http.get<News[]>(`${this.constants.getApiUrl()}/api/news`);
  }

  /**
   * Sends request to backend to add news
   * @param news - News Object
   * @param token - JWT
   */
  public addNews(news: NewsAddEdit, token: string): Observable<News>{
    const headers = { 'Content-Type': 'application/json', 'Authorization': `Bearer ${token}` };
    return this.http.post<News>(`${this.constants.getApiUrl()}/api/news`, news, {headers});
  }

  /**
   * Sends request to backend to edit news
   * @param news - News Object
   * @param newsId - News Id
   * @param token - JWT
   */
  public editNews(news: NewsAddEdit, newsId: number, token: string): Observable<News>{
    const headers = { 'Content-Type': 'application/json', 'Authorization': `Bearer ${token}` };
    return this.http.put<News>(`${this.constants.getApiUrl()}/api/news/${newsId}`, news, {headers});
  }

  /**
   * Gets list of news that user had added
   * @param userId - Users Id
   * @param token - JWT
   */
  public getUserAddedNews(userId: string, token: string): Observable<News[]>{
    const headers = { 'Content-Type': 'application/json', 'Authorization': `Bearer ${token}` };
    return this.http.get<News[]>(`${this.constants.getApiUrl()}/api/news/${userId}/added`, {headers});
  }

  /**
   * Gets list of news that user had edited
   * @param userId - Users Id
   * @param token - JWT
   */
  public getUserEditedNews(userId: string, token: string): Observable<News[]>{
    const headers = { 'Content-Type': 'application/json', 'Authorization': `Bearer ${token}` };
    return this.http.get<News[]>(`${this.constants.getApiUrl()}/api/news/${userId}/edited`, {headers});
  }

  /**
   * Sets private variable news
   * @param news - News Object
   */
  public setNews(news: News): void{
    this.news = news;
  }

  /**
   * Gets private variable news
   */
  public getNews(): News{
    return this.news;
  }

}
