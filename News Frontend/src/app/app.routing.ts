import { MyNewsComponent } from './my-news/my-news.component';
import { AddEditComponent } from './add-edit/add-edit.component';
import { NgModule } from '@angular/core';
import { CommonModule, } from '@angular/common';
import { BrowserModule  } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { ProfileComponent } from './profile/profile.component';
import { SignupComponent } from './signup/signup.component';
import { LandingComponent } from './landing/landing.component';
import { LoginComponent } from './login/login.component';

const routes: Routes =[
    { path: 'home',             component: HomeComponent },
    { path: 'user-profile',     component: ProfileComponent },
    { path: 'register',           component: SignupComponent },
    { path: 'news',          component: LandingComponent },
    { path: 'news/:categoryId',          component: LandingComponent },
    { path: 'news/me/:mode',          component: MyNewsComponent },
    { path: 'news/add-edit/:mode',          component: AddEditComponent },
    { path: 'login',          component: LoginComponent },
    { path: '', redirectTo: 'news', pathMatch: 'full' },
    { path: '**', redirectTo: 'news', pathMatch: 'full' }
];

@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    RouterModule.forRoot(routes)
  ],
  exports: [
  ],
})
export class AppRoutingModule { }
