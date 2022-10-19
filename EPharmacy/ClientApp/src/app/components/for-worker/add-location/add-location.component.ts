import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {PharmacyClient, PharmacyLocationRequest} from "../../../api/epharmacy";
import {Router} from "@angular/router";

@Component({
  selector: 'app-add-location',
  templateUrl: './add-location.component.html',
  styleUrls: ['./add-location.component.css']
})
export class AddLocationComponent implements OnInit {

  additionForm: FormGroup;
  submitted = false;

  readonly latitude: number = 51.1170138;
  readonly longitude: number = 17.0376042;
  markers = [];

  constructor(private formBuilder: FormBuilder,
              private locationClient: PharmacyClient,
              private router: Router) { }

  ngOnInit() {
    this.additionForm = this.formBuilder.group({
      name: ['', Validators.required],
      address: ['', Validators.required],
      latitude: ['', Validators.required],
      longitude:['', Validators.required],
    });
  }

  onSubmit() {
    this.submitted = true;
    if (this.additionForm.invalid) return;

    this.locationClient.addLocation(PharmacyLocationRequest.fromJS(this.additionForm.value)).subscribe(
      () => this.router.navigate(['/location-list'])
    );
  }

  addMarker(lat: number, lng: number) {
    this.markers.length = 0;
    this.markers.push({lat, lng});
    this.additionForm.controls["latitude"].setValue(lat);
    this.additionForm.controls["longitude"].setValue(lng);
  }

  get f() { return this.additionForm.controls; }
}
