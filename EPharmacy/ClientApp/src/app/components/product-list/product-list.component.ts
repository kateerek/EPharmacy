import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import * as _ from 'lodash';
import { ShoppingCartService } from '../../services/shopping/shopping-cart.service';
import { AttributeClient, ProductClient, ProductDetailsListModel, CategoryModel } from '../../api/epharmacy';
import { switchMap, tap } from 'rxjs/operators';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css',
              '../../styles/common.css']
})
export class ProductListComponent implements OnInit, OnDestroy {
  products: ProductDetailsListModel[] = [];
  filteredProducts: ProductDetailsListModel[] = [];
  productsOnPage: ProductDetailsListModel[] = [];
  pageList: number[];
  offset = 10;
  page = 1;
  categories: CategoryModel[] = [];
  categoriesSelected: number[] = [1];
  expandedCategories: number[] = [];
  productsOffer;
  subscription: Subscription;
  text = '';
  constructor(private shopping: ShoppingCartService,
              private attribute: AttributeClient,
              private productList: ProductClient ) { }
  ngOnInit() {
    this.productList.productsByAttributes([9]).subscribe( offer => this.productsOffer = offer.slice(0, 4));
    this.getProducts();
  }
  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
  getCategories() {
    return this.attribute.getCategories();
  }
  isPaginationVisible() {
    return this.getPageNumber() > 1;
  }
  getPageNumber() {
    return this.filteredProducts.length / this.offset;
  }
  setPageArray() {
    this.pageList = [];
    for (let i = 0; i < this.getPageNumber(); i++) {
      this.pageList.push(i + 1);
    }
  }
  getLast(value) {
    if (value + this.offset > this.filteredProducts.length - 1) {
      return this.filteredProducts.length ;
    } else {
      return value + this.offset;
    }
  }
  setPage(value) {
    this.page = value;
    this.productsOnPage = this.filteredProducts.slice((this.page - 1) * this.offset, this.getLast((this.page - 1) * this.offset));
  }
  nextPage() {
    if (this.page < this.getPageNumber()) {
      this.setPage(this.page + 1);
    }

  }
  prevPage() {
    if (this.page - 1 !== 0) {
    this.setPage(this.page - 1);
    }
  }

  getProductsByAttributes(attributes: number[]) {
    return this.productList.productsByAttributes(attributes);
  }

  getProducts() {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
    this.subscription = this.getCategories().pipe(
      tap(categories => this.categories = categories),
      switchMap((categories) =>
        this.getProductsByAttributes(this.categoriesSelected)
      )).subscribe(products => {
      if (products !== null && products.length !== 0) {
        this.products = products;
        this.filteredProducts = products;
        this.setPage(1);
      } else {
        this.products = [];
        this.filteredProducts = [];
        this.productsOnPage = [];
      }
      this.setPageArray();
      this.page = 1;
    });
  }
  onChange(input) {
    this.shopping.toggle(input, 1);
  }

  filter(attributeId: number) {
    if (!(this.categoriesSelected.length == 1 && this.categoriesSelected.includes(attributeId))) {
      this.categoriesSelected = [attributeId];
      this.getProducts();
    }
  }

  filterSub(attributeId: number, subAttributeId: number) {
    if (!(this.categoriesSelected.includes(attributeId) && this.categoriesSelected.includes(subAttributeId))) {
      this.categoriesSelected = [attributeId, subAttributeId];
      this.getProducts();
    }
  }

  filterProducts(data: string) {
    if (data) {
      this.filteredProducts = this.products.filter(prod => {
        return prod.name.toLowerCase().indexOf(data.toLowerCase()) > -1;
      });
    } else {
      this.filteredProducts = this.products;
    }
    this.setPage(1);
    this.setPageArray();
    this.page = 1;
  }
  addLike(value) {
    value.isFavourite = true;
    this.productList.changeIsFavouriteStatus(value.id).subscribe();
  }
  removeLike(value) {
    value.isFavourite = false;
    this.productList.changeIsFavouriteStatus(value.id).subscribe();
  }
}
