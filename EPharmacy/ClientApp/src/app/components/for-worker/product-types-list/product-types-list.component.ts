import { Component, OnInit } from '@angular/core';
import { ListBase } from '../list-base';
import { ProductTypeModel, ProductClient } from '../../../api/epharmacy';

@Component({
  selector: 'app-product-types-list',
  templateUrl: './product-types-list.component.html',
  styleUrls: ['./product-types-list.component.css']
})
export class ProductTypesListComponent extends ListBase<ProductTypeModel> implements OnInit {

  constructor(private products: ProductClient) {
    super();
  }

  ngOnInit() {
    super.getValues(this.products.getAllProductTypes());
  }

}
