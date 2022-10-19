import { Component, OnInit } from '@angular/core';
import * as _ from 'lodash';
import { ShoppingCartService } from '../../../services/shopping/shopping-cart.service';
import { AttributeClient, ProductClient, AttributeResponseModel, ProductDetailsListModel } from '../../../api/epharmacy';
import { switchMap } from 'rxjs/operators';
import { Observable } from 'rxjs/observable';
import { ListBase } from '../list-base';
import { Router } from '@angular/router';

@Component({
  selector: 'app-prod-list',
  templateUrl: './prod-list.component.html',
  styleUrls: ['./prod-list.component.css']
})
export class ProdListComponent extends ListBase<ProductDetailsListModel> implements OnInit {
  types: Observable<AttributeResponseModel[]>;
  typeID = 0;

  constructor(private shopping: ShoppingCartService,
    private attribute: AttributeClient,
    private router: Router,
    private productList: ProductClient) {
    super();
  }

  ngOnInit() {
    this.getTypes();
    this.getProducts();
  }

  getTypes() {
    this.types = this.attribute.getAll();
  }

  getProducts() {
    super.getValues(this.types.pipe(switchMap(types => this.productList.productsByAttributes([types[this.typeID].id]))));
  }

  onChange(input) {
    this.shopping.toggle(input, 1);
  }
  delete(id) {
    this.productList.remove(id).subscribe( remove => this.getProducts());
  }
  filter(filterVal: any) {
    if (!(this.typeID === filterVal)) {
      this.typeID = filterVal;
      this.getProducts();
    }
  }
}


