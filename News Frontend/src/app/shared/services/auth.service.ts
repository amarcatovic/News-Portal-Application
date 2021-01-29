import { Router } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import { ConstantsService } from './constants.service';
import { HttpClient } from '@angular/common/http';
import { Injectable, Output } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { LoginResponse } from '../models/LoginResponse';
import { User } from '../models/User';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

@Output() userLogedInSubject: Subject<LoginResponse> = new Subject<LoginResponse>();
@Output() userLogOffSubject: Subject<boolean> = new Subject<boolean>();

constructor(private http: HttpClient, 
  private constants: ConstantsService,
  private cookieService: CookieService,
  private router: Router,) { }

/**
 * Authentificates user
 * @param email - users email
 * @param passwordRaw - users password
 */
public authenticateUser(email: string, passwordRaw: string): void{
  const body: any = {email: email, passwordRaw: passwordRaw};
  const headers = { 'Content-Type': 'application/json' };
  this.http.post<LoginResponse>(`${this.constants.getApiUrl()}/api/auth/login`, body, {headers})
    .subscribe(response => {
      this.cookieService.set("token", response.token);
      this.cookieService.set("user", JSON.stringify(response.user));
      this.userLogedInSubject.next(response);
      this.router.navigate(['/']);
    });
}

/**
 * Logs user off - Deletes cookies
 */
public logUserOff(): void{
    this.cookieService.deleteAll();
    this.userLogOffSubject.next(true);
    this.router.navigate(['/']);
}

}
