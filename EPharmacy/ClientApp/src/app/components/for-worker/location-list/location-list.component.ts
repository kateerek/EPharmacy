import { Component, OnInit } from '@angular/core';
import {PharmacyLocationModel, PharmacyClient} from '../../../api/epharmacy';
import { ListBase } from '../list-base';

@Component({
  selector: 'app-location-list',
  templateUrl: './location-list.component.html',
  styleUrls: ['./location-list.component.css']
})
export class LocationListComponent extends ListBase<PharmacyLocationModel> implements OnInit {

  selectedLocation: PharmacyLocationModel;


  constructor(private locationList: PharmacyClient){
    super();
  }

  ngOnInit() {
    this.getLocations();
  }

  getLocations(){
   super.getValues(this.locationList.getLocations());
  }

  onEdit(newLocationValues: any) {
    this.selectedLocation.init(newLocationValues);
    this.selectedLocation = null;
  }

  onDelete(location){
    this.locationList.deleteLocation(location.id).subscribe(_ => this.getLocations());
  }


}
