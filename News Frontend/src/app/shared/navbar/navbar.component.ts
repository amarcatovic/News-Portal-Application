import { AuthService } from './../services/auth.service';
import { CookieService } from 'ngx-cookie-service';
import { CategoriesService } from './../services/categories.service';
import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd, NavigationStart } from '@angular/router';
import { Location, PopStateEvent } from '@angular/common';
import { Category } from '../models/Category';
import { User } from '../models/User';

@Component({
    selector: 'app-navbar',
    templateUrl: './navbar.component.html',
    styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
    public isCollapsed = true;
    private lastPoppedUrl: string;
    private yScrollStack: number[] = [];

    public user: User = null;

    public categoriesArray: Category[] = [];

    constructor(public location: Location, 
        private router: Router,
        private categoriesService: CategoriesService,
        private cookieService: CookieService,
        private authService: AuthService,) {
    }

    ngOnInit() {
      this.router.events.subscribe((event) => {
        this.isCollapsed = true;
        if (event instanceof NavigationStart) {
           if (event.url != this.lastPoppedUrl)
               this.yScrollStack.push(window.scrollY);
       } else if (event instanceof NavigationEnd) {
           if (event.url == this.lastPoppedUrl) {
               this.lastPoppedUrl = undefined;
               window.scrollTo(0, this.yScrollStack.pop());
           } else
               window.scrollTo(0, 0);
       }
       if(this.cookieService.check('user')){
           this.user = JSON.parse(this.cookieService.get('user'));
       }
       this.authService.userLogedInSubject.subscribe(response => {
         this.user = response.user;
       });
       this.authService.userLogOffSubject.subscribe(response => {
         this.user = null;
       });
     });
     this.location.subscribe((ev:PopStateEvent) => {
         this.lastPoppedUrl = ev.url;
     });

     this.categoriesService.getCategories().subscribe(response => {
         this.categoriesArray = response;
     })
    }

    isHome() {
        var titlee = this.location.prepareExternalUrl(this.location.path());

        if( titlee === '#/home' ) {
            return true;
        }
        else {
            return false;
        }
    }
    isDocumentation() {
        var titlee = this.location.prepareExternalUrl(this.location.path());
        if( titlee === '#/documentation' ) {
            return true;
        }
        else {
            return false;
        }
    }

    /**
     * Loggs user off
     */
    public logOff(): void{
        this.authService.logUserOff();
        this.router.navigate(['/']);
    }
}
