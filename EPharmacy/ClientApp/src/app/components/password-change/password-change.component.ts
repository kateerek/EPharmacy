import { Component, OnInit } from '@angular/core';
import { getPasswordValidationErrorMessage } from '../../directives/password-validator';
import { Location } from '@angular/common';
import { AccountClient, PasswordChangeRequest } from '../../api/epharmacy';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-password-change',
  templateUrl: './password-change.component.html',
  styleUrls: ['./password-change.component.css']
})
export class PasswordChangeComponent implements OnInit {

  validationErrorMessagesFactory = getPasswordValidationErrorMessage;
  passwordChangeData = new PasswordChangeRequest();

  wasPasswordChangeSuccessful = false;
  errors: string;

  constructor(private location: Location, private account: AccountClient) { }

  ngOnInit() {
  }

  changePassword(form: NgForm) {
    this.account.changePassword(this.passwordChangeData).subscribe(
      () => {
        this.wasPasswordChangeSuccessful = true;
        this.errors = '';
        form.resetForm();
      },
      error => {
        this.wasPasswordChangeSuccessful = false;
        this.errors = error.message;
        form.resetForm();
      }
    )
  }

  goBack() {
    this.location.back();
  }

}
