import { CookieService } from 'ngx-cookie-service';
import { Category } from './../shared/models/Category';
import { CategoriesService } from './../shared/services/categories.service';
import { NewsService } from './../shared/services/news.service';
import { NewsAddEdit } from './../shared/models/NewsAddEdit';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { News } from '../shared/models/News';
import { User } from '../shared/models/User';

@Component({
  selector: 'app-add-edit',
  templateUrl: './add-edit.component.html',
  styleUrls: ['./add-edit.component.css']
})
export class AddEditComponent implements OnInit {
  public focus: any;
  public focus1: any;

  private user: User = null;
  private token: string;

  news: News = null;
  newsAddEdit: NewsAddEdit = new NewsAddEdit();

  categoryList: Category[] = [];

  mode: string;

  form: FormGroup;

  constructor(private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private newsService: NewsService,
    private categoryService: CategoriesService,
    private cookieService: CookieService,) { }

  ngOnInit(): void {
    this.mode = this.route.snapshot.params['mode'];
    if(this.mode === 'edit'){
      this.news = this.newsService.getNews();
    }
    else if(this.mode !== 'edit' && this.mode !== 'add'){
      this.router.navigate(['/']);
    }

    this.categoryService.getCategories().subscribe(response => {
      this.categoryList = response;
      this.initForm();
    });

    this.user = JSON.parse(this.cookieService.get('user'));
    this.token = this.cookieService.get('token');

  }

  /**
   * Initializes the form. If the page is in edit mode, it initializes text fields to match news from service
   */
  private initForm(){

    let title: string = '';
    let content: string = '';
    let categoryId: number = 1;
    let userId: string = this.user.id;

    if(this.news){
      title = this.news.title;
      content = this.news.content;
      categoryId = this.categoryList.find(c => c.name === this.news.category).categoryId;
    }

    this.form = this.fb.group({
      title: [title, [Validators.required]],
      content: [content, [Validators.required]],
      categoryId: [categoryId, [Validators.required]],
      userId: [userId, [Validators.required]]
    });
  }

  /**
   * Calls methods in news service to add or edit news
   */
  public submit(): void{
    if(!this.form.valid){
      return;
    }

    if(this.mode === 'add'){
      this.newsService.addNews(this.form.value, this.token)
        .subscribe(response => {
          if(response.newsId){
            this.router.navigate(["/news"]);
          }
        });
    }

    if(this.mode === 'edit'){
      this.newsService.editNews(this.form.value, this.news.newsId, this.token)
        .subscribe(response => {
          this.router.navigate(["/news"]);
        });
    }
  }

}
