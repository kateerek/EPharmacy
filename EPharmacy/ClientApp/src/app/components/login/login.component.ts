import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NgForm } from '@angular/forms';
import { LoginRequest } from '../../api/epharmacy';
import { AuthService } from '../../services/auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  errors: string;

  brandNew: boolean;
  registeredUserEmail: string;

  constructor(
    private router: Router,
    private authService: AuthService,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.brandNew = 'brandNew' in params ? params['brandNew'] as boolean : false;
      this.registeredUserEmail = 'email' in params ? params['email'] : '';
    });
  }

  login(loginForm: NgForm) {
    this.authService.login(LoginRequest.fromJS(loginForm.value)).subscribe(
      _ => this.router.navigate(['/']),
      error => {
        console.log('Login error: ', error);
        this.errors = 'Invalid email or password!';
        loginForm.resetForm(loginForm.value);
      }
    );
  }
}
