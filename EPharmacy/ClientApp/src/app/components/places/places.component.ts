import { Component, OnInit } from '@angular/core';
import { PharmacyClient, PharmacyLocationModel } from '../../api/epharmacy';
import {ListBase} from "../for-worker/list-base";


@Component({
  selector: 'app-places',
  templateUrl: './places.component.html',
  styleUrls: ['./places.component.css',
              '../../styles/common.css']
})
export class PlacesComponent extends ListBase<PharmacyLocationModel> implements OnInit {

  readonly defaultLat = 51.107883;
  readonly defaultLng = 17.038538;
  lat: number = this.defaultLat;
  lng: number = this.defaultLng;
  openedMarker: number = 0;

  constructor(private pharmacyLocation : PharmacyClient){
    super();
  }

  ngOnInit() {
    this.updatePlaces();
  }

  updatePlaces() {
    super.getValues(this.pharmacyLocation.getLocations());
  }

  setCurrentFocused(id: number, latitude: number, longitude: number) {
    this.openMarker(id);
    this.lat = latitude;
    this.lng = longitude;
  }

  openMarker(id: number) {
    if (id !== undefined) {
      this.openedMarker = id;
    }
  }

  isMarkerOpen(id : number) {
    return this.openedMarker == id;
  }

  setInitialFocused(){
    if (this.valuesOnPage !== undefined && this.valuesOnPage.length > 0) {
      const place = this.valuesOnPage[0];
      this.setCurrentFocused(0, place.latitude, place.longitude);
    }
    else {
      this.setCurrentFocused(undefined, this.defaultLat, this.defaultLng);
    }
  }

  setPage(value: number) {
    super.setPage(value);
    this.setInitialFocused();
  }
}
