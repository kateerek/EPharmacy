import { Injectable, Output, EventEmitter} from '@angular/core';
import { IProductDetailsListModel, ProductClient } from '../../api/epharmacy';

@Injectable()
export class ShoppingCartService {
  shoppingList: CartElement[];
  @Output() change: EventEmitter<CartElement[]> = new EventEmitter();
  constructor(private prod: ProductClient) {
    this.shoppingList = this.getLocalCart();
    if (this.shoppingList !== null && this.shoppingList.length > 0) {
      this.shoppingList.sort((left, right) => {
        if (left.product.id < right.product.id) {
          return -1;
        }
        return 1;
      });
      prod.getDetailsForProducts(this.toIdArray()).subscribe(element => {
        element.forEach((item, index) => {
          this.shoppingList[index].product = item;
        });
      });
      this.setLocalCart();
    }
  }

  toggle(value: IProductDetailsListModel, number: number) {
    const index = this.shoppingList.findIndex(x => x.product.id === value.id);
    if (index !== -1) {
      if (number > 0 || this.shoppingList[index].counter > 1) {
        this.shoppingList[index].counter += number;
        this.setLocalCart();
      } else {
        this.shoppingList.splice(index, 1);
        this.setLocalCart();
      }
    } else {
      this.shoppingList.push({ product: value, counter: 1, selectedDiscount: null });
      this.setLocalCart();
    }
    this.change.emit(this.shoppingList);
  }
  private getLocalCart() {
    const localStorageItem = JSON.parse(localStorage.getItem('shoppingCart'));
    return localStorageItem == null ? [] : localStorageItem.shoppingCart;
  }
  private setLocalCart() {
    localStorage.setItem('shoppingCart', JSON.stringify({ shoppingCart: this.shoppingList }));
  }
  clear() {
    this.shoppingList = [];
    localStorage.setItem('shoppingCart', null);
    this.change.emit(this.shoppingList);
  }
  toIdArray() {
    const id = [];
    this.shoppingList.forEach(element => {
      id.push(element.product.id);
    });
    return id;
  }
  updateDiscountForProduct(productId: number, discountId: number) {
    const index = this.shoppingList.findIndex(x => x.product.id === productId);
    if (index !== -1) {
      this.shoppingList[index].selectedDiscount = discountId;
      this.setLocalCart();
    }
  }
}

interface CartElement {
  product: IProductDetailsListModel;
  counter: number;
  selectedDiscount?: number;
}
