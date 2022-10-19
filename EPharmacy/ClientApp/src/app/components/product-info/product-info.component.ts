import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { switchMap, map } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';
import { ProductClient, ProductDetailsModel, IProductDetailsListModel, ProductActiveSubstanceResponse } from '../../api/epharmacy';
import { ShoppingCartService } from '../../services/shopping/shopping-cart.service';

@Component({
  selector: 'app-product-info',
  templateUrl: './product-info.component.html',
  styleUrls: ['./product-info.component.css',
              '../../styles/common.css']
})
export class ProductInfoComponent implements OnInit {
  id: number;
  product: ProductDetailsModel;
  prescriptionDiscounts;
  productsOffer;
  isFav: Boolean;
  text = 'Substytuty:';
  constructor(private route: ActivatedRoute,
              private router: Router,
              private shopping: ShoppingCartService,
              private productList: ProductClient) { }

  ngOnInit() {
     this.route.paramMap.pipe(
      switchMap((params: ParamMap) =>
      this.productList.details(this.id = Number(params.get('id')))
    )).subscribe( obj => {
      this.product = obj;
      this.prescriptionDiscounts = obj.productDiscounts.prescriptionDiscounts;
    });
     this.route.paramMap.pipe(
      switchMap((params: ParamMap) =>
      this.productList.getProductSubstitutes(Number(params.get('id')))
    )).subscribe( offer => this.productsOffer = offer.slice(0, 4));
  this.productList.getProductSubstitutes(11).subscribe(elo => console.log(elo));
  }

  addToCart(input: ProductDetailsModel) {
    const prod: IProductDetailsListModel = {id: this.id , name: input.name,  productPrice: input.productPrice,
                                            imageUrl: input.imageUrl, productDiscounts: input.productDiscounts};
    this.shopping.toggle( prod , 1);
  }
  addLike() {
    this.isFav = true;
    this.productList.changeIsFavouriteStatus(this.id).subscribe();
  }
  isFavourite(prod: ProductDetailsModel) {
    if (this.isFav === undefined) {
      this.isFav = prod.isFavourite;
    }
    return this.isFav;
  }
  removeLike() {
    this.isFav = false;
    this.productList.changeIsFavouriteStatus(this.id).subscribe();
  }
}
