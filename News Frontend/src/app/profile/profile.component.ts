import { AuthService } from './../shared/services/auth.service';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { Component, OnInit } from '@angular/core';
import { User } from '../shared/models/User';

@Component({
    selector: 'app-profile',
    templateUrl: './profile.component.html',
    styleUrls: ['./profile.component.scss']
})

export class ProfileComponent implements OnInit {

    public user: User = null;

    constructor(private cookieService: CookieService,
        private router: Router,
        private authService: AuthService,) { }

    ngOnInit() {
        if(this.cookieService.check('user')){
            this.user = JSON.parse(this.cookieService.get('user'));
        }else{
            this.router.navigate(['/']);
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
