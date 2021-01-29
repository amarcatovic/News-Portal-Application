import { CookieService } from 'ngx-cookie-service';
import { NewsService } from './../shared/services/news.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { News } from '../shared/models/News';
import { User } from '../shared/models/User';

@Component({
  selector: 'app-my-news',
  templateUrl: './my-news.component.html',
  styleUrls: ['./my-news.component.css']
})
export class MyNewsComponent implements OnInit {

  public newsList: News[] = [];
  public mode: string;

  private token: string;
  private user: User;

  constructor(private newsService: NewsService,
    private route: ActivatedRoute,
    private router: Router,
    private cookieService: CookieService) { }

  ngOnInit(): void {
    this.token = this.cookieService.get('token');
    this.user = JSON.parse(this.cookieService.get('user'));

    this.route.paramMap.subscribe(params => {
      this.mode = params.get('mode');
      if(this.mode === 'added'){
        this.newsService.getUserAddedNews(this.user.id, this.token).subscribe(response => {
          this.newsList = response;
        });
      }else if(this.mode === 'edited'){
        this.newsService.getUserEditedNews(this.user.id, this.token).subscribe(response => {
          this.newsList = response;
        });
      }
    });
  }

  /**
   * Sets news varable in news service and redirects user to edit news
   * @param news - News Object
   */
  public editNews(news: News){
    this.newsService.setNews(news);
    this.router.navigate(['/news/add-edit/edit']);
  }
}
