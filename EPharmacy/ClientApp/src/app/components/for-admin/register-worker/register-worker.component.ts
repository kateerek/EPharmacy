import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AccountClient, RegistrationRequest } from '../../../api/epharmacy';
import { Router } from '@angular/router';
import { getPasswordValidationErrorMessage } from '../../../directives/password-validator';

@Component({
  selector: 'app-register-worker',
  templateUrl: './register-worker.component.html',
  styleUrls: ['./register-worker.component.css']
})
export class RegisterWorkerComponent implements OnInit {

  errors: string;
  passwordValidationMessageFactory = getPasswordValidationErrorMessage;

  constructor(private accountClient : AccountClient, private router : Router) { }

  ngOnInit() {
  }

  registerWorker(registerForm: NgForm) {
    const workerEmail = registerForm.value.email;

    this.accountClient.createWorker(RegistrationRequest.fromJS(registerForm.value))
      .subscribe(
        _ => this.router.navigate(['/login'], { queryParams: { brandNew: true, email: workerEmail } }),
        error => {
          this.errors = error.message;
          registerForm.resetForm(registerForm.value);
        }
      );
  }
}
