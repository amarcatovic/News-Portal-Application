import { MyNewsComponent } from './my-news/my-news.component';
import { AddEditComponent } from './add-edit/add-edit.component';
import { NgModule } from '@angular/core';
import { CommonModule, } from '@angular/common';
import { BrowserModule  } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';

import { ProfileComponent } from './profile/profile.component';
import { LandingComponent } from './landing/landing.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './shared/guards/auth.guard';

const routes: Routes =[
    { path: 'user-profile',     component: ProfileComponent, canActivate: [AuthGuard] },
    { path: 'news',          component: LandingComponent },
    { path: 'news/:categoryId',          component: LandingComponent },
    { path: 'news/me/:mode',          component: MyNewsComponent, canActivate: [AuthGuard] },
    { path: 'news/add-edit/:mode',          component: AddEditComponent, canActivate: [AuthGuard] },
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
