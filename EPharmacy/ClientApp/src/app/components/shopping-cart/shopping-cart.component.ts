import { Component, OnInit, OnDestroy } from '@angular/core';
import { ShoppingCartService } from '../../services/shopping/shopping-cart.service';
import { Router } from '@angular/router';
import { PharmacyLocationModel, PharmacyClient,  SalesOrderResponse,
         SalesOrderClient, SalesOrderRequest, ProductItemRequest, ProductDetailsModel } from '../../api/epharmacy';
import { Observable } from 'rxjs/observable';
import { AuthService } from '../../services/auth/auth.service';


@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.css']
})
export class ShoppingCartComponent implements OnInit, OnDestroy {

  shoppingList = [];
  shoppingSubscirption;
  location = -1;
  order = new SalesOrderRequest();
  pharmacyLocations: Observable<PharmacyLocationModel[]>;
  constructor(private shopping: ShoppingCartService,
    private router: Router,
    private pharmacy: PharmacyClient,
    private salesOrder: SalesOrderClient,
    private authService: AuthService) { }

  ngOnInit() {
    this.pharmacyLocations = this.pharmacy.getLocations();
    this.shoppingList = this.shopping.shoppingList;
    this.shoppingSubscirption = this.shopping.change.subscribe(shoppingList => {
      this.shoppingList = shoppingList;
      if (shoppingList.length === 0) {
        this.router.navigate(['/']);
      }
    });
  }
  onChange(input, counter) {
    this.shopping.toggle(input, counter);
  }
  currentPrice(product: ProductDetailsModel, prescriptionDiscountId?: number) {
    if (prescriptionDiscountId !== null) {
      const idx = product.productDiscounts.prescriptionDiscounts.findIndex(x => x.id == prescriptionDiscountId);
      return product.productDiscounts.prescriptionDiscounts[idx].price;
    }
    if (product.productDiscounts.offerDiscount) {
      return product.productDiscounts.offerDiscount.price;
    }
    return product.productPrice;
  }

  currentDiscount(product: ProductDetailsModel, prescriptionDiscountId?: number) {
    if (prescriptionDiscountId !== null) {
      return prescriptionDiscountId;
    }
    if (product.productDiscounts.offerDiscount == null) {
      return null;
    }
    return product.productDiscounts.offerDiscount.id;
  }

  price(): number {
    let price = 0;
    this.shoppingList.forEach(elem => {
      price += this.currentPrice(elem.product, elem.selectedDiscount ) * elem.counter;
    });
    return price;
  }
    ngOnDestroy() {
    this.shoppingSubscirption.unsubscribe();
  }
  setLocation(event) {
    this.location = event;
  }
  createOrder() {
    this.order.items = [];
    this.shoppingList.forEach(elem => {
      const prod = new ProductItemRequest();
      prod.itemCount = elem.counter;
      prod.productId = elem.product.id;
      prod.discountId = this.currentDiscount(elem.product, elem.selectedDiscount);
      this.order.items.push(prod);
    });
  }
  sendOrder() {
    if (this.location === -1) {
      return;
    } else {
      this.createOrder();
      this.order.pharmacyLocationId = this.location;
      this.salesOrder.create(this.order).subscribe(_ => this.shopping.clear(),  error => console.log(error));
    }

  }
  isVisible() {
    return this.authService.isLoggedIn();
  }

  setSelectedDiscount(productId: number, discountId: number) {
    console.log(productId, discountId);
    this.shopping.updateDiscountForProduct(productId, discountId);
    this.forceReload();
  }

  forceReload() {
    this.router.navigateByUrl('/DummyComponent', { skipLocationChange: true });
    this.router.navigate(["ShoppingCartComponent"]);
  }
}

