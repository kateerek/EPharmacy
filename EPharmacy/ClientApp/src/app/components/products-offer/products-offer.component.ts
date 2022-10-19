import { Component, OnInit, Input } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { ProductDetailsListModel } from '../../api/epharmacy';

@Component({
  selector: 'app-products-offer',
  templateUrl: './products-offer.component.html',
  styleUrls: ['./products-offer.component.css',
              '../../styles/common.css']
})
export class ProductsOfferComponent implements OnInit {
  @Input() productsOffer: ProductDetailsListModel[];
  @Input() text;
  constructor() {
   }

  ngOnInit() {
  }

}
