import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AccountClient, RegistrationRequest } from '../../api/epharmacy';
import { Router } from '@angular/router';
import { getPasswordValidationErrorMessage } from '../../directives/password-validator';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  errors: string;
  passwordValidationMessageFactory = getPasswordValidationErrorMessage;

  constructor(private accountClient : AccountClient, private router : Router) { }

  ngOnInit() {
  }

  registerUser(registerForm: NgForm) {
    const userEmail = registerForm.value.email;

    this.accountClient.create(RegistrationRequest.fromJS(registerForm.value))
      .subscribe(
        _ => this.router.navigate(['/login'], { queryParams: { brandNew: true, email: userEmail } }),
        error => {
          this.errors = error.message;
          registerForm.resetForm(registerForm.value);
        }
      );
  }
}
