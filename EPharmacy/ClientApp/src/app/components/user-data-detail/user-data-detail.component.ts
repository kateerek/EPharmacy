import { Component, OnInit } from '@angular/core';
import { AccountClient, UserData } from '../../api/epharmacy';
import { Location } from '@angular/common';
import { Router, ActivatedRoute } from '@angular/router';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-user-data-detail',
  templateUrl: './user-data-detail.component.html',
  styleUrls: ['./user-data-detail.component.css']
})
export class UserDataDetailComponent implements OnInit {
  userData = new UserData();
  errors: string;
  wasDataChangeSuccessful = false;

  constructor(private client: AccountClient, private location: Location) { }

  ngOnInit() {
    this.client.getData().subscribe(userData => this.userData = userData, error => this.errors = error.message);
  }

  goBack() {
    this.location.back();
  }

  updateUserData(form: NgForm) {
    this.client.changeData(this.userData).subscribe(
      () => {
        this.wasDataChangeSuccessful = true;
        this.errors = '';
        form.form.markAsPristine();
      },
      (error) => {
        this.wasDataChangeSuccessful = false;
        this.errors = error.message;
      }
    );
  }

}
