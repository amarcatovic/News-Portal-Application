import { ActivatedRoute, Router } from '@angular/router';
import { News } from './../shared/models/News';
import { NewsService } from './../shared/services/news.service';
import { CookieService } from 'ngx-cookie-service';
import { Component, OnInit } from '@angular/core';
import { User } from '../shared/models/User';
import { CategoriesService } from '../shared/services/categories.service';
import { AuthService } from '../shared/services/auth.service';

@Component({
    selector: 'app-landing',
    templateUrl: './landing.component.html',
    styleUrls: ['./landing.component.scss']
})

export class LandingComponent implements OnInit {
  focus: any;
  focus1: any;

  public newsList: News[] = [];

  public user: User = null;
  public searchTerm: string = '';

  public categoryId: number;

  constructor(private cookieService: CookieService,
    private newsService: NewsService,
    private categoriesService: CategoriesService,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,) { }

  ngOnInit() {
    if(this.cookieService.check('user')){
      this.user = JSON.parse(this.cookieService.get('user'));
    }

    this.route.paramMap.subscribe(params => {
      this.categoryId = this.route.snapshot.params['categoryId'];
      if(this.categoryId){
        this.categoriesService.getCategoryWithNews(this.categoryId).subscribe(response => {
          this.newsList = response.news;
        });
      }
      else{
        this.newsService.getAllNews().subscribe(response => {
          this.newsList = response;
        });
      }
    });

    this.authService.userLogOffSubject.subscribe(response => {
      this.user = null;
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
