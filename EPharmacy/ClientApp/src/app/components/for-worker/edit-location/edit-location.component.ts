import { Component, Input, Output, OnInit, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import {PharmacyClient, PharmacyLocationModel} from "../../../api/epharmacy";

@Component({
  selector: 'app-edit-location',
  templateUrl: './edit-location.component.html',
  styleUrls: ['./edit-location.component.css']
})
export class EditLocationComponent implements OnInit {

  @Input() location: PharmacyLocationModel;
  @Output() locationChange = new EventEmitter<any>();

  editionForm: FormGroup;
  submitted = false;

  readonly latitude: number = 51.1170138;
  readonly longitude: number = 17.0376042;
  markers = [];

  constructor(private formBuilder: FormBuilder,
              private locationClient: PharmacyClient) { }

  ngOnInit() {
    if (this.location === null) return;

    this.editionForm = this.formBuilder.group({
      name: [this.location.name, Validators.required],
      address: [this.location.address, Validators.required],
      latitude: [this.location.latitude, Validators.required],
      longitude: [this.location.longitude, Validators.required]
    });
  }

  addMarker(lat: number, lng: number) {
    this.markers.length = 0;
    this.markers.push({lat, lng});
    this.editionForm.controls["latitude"].setValue(lat);
    this.editionForm.controls["longitude"].setValue(lng);
  }

  onSubmit() {
    this.submitted = true;
    if (this.editionForm.invalid) return;

    let id = this.location.id;
    let pharmacyLocationModel = this.editionForm.value;
    pharmacyLocationModel['id'] = id;


    this.locationClient.editLocation(id, PharmacyLocationModel.fromJS(pharmacyLocationModel))
      .subscribe(() => this.locationChange.emit(pharmacyLocationModel));
  }

  get f() { return this.editionForm.controls; }

}
