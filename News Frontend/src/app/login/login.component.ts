import { AuthService } from './../shared/services/auth.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public focus: any;
  public focus1: any;

  loginForm: FormGroup;

  constructor(private authService: AuthService,
    private fb: FormBuilder,) { }

  ngOnInit() {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(7)]]
    });
  }


  public signUserIn(): void{
    console.log("ušlo")
    this.authService.authenticateUser(this.loginForm.get('email').value, this.loginForm.get('password').value);
  }


}
