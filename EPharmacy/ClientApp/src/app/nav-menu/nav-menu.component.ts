import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth/auth.service';
import { Router } from '@angular/router';
import { ShoppingCartService } from '../services/shopping/shopping-cart.service';


@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css',
              '../styles/common.css']
})
export class NavMenuComponent implements OnInit {
  email: string;
  shoppingList = 0;
  shoppingSubscirption;
  constructor(private authService: AuthService, private router: Router, private shopping: ShoppingCartService) {

   }

  ngOnInit() {
    this.email = this.authService.getUserEmail();
    this.authService.loginStatusChanged.subscribe(() => {
      this.email = this.authService.getUserEmail();
    });
    let list = 0;
    this.shopping.shoppingList.forEach(element => list += element.counter);
    this.shoppingList = list;
    this.shoppingSubscirption = this.shopping.change.subscribe(shoppingList => {
        list = 0;
        shoppingList.forEach(element => list += element.counter);
        this.shoppingList = list;
      });
  }
goToCart() {
  if (this.shoppingList > 0) {
    this.router.navigate(['/shopping-cart']);
  }

}
  logout() {
    this.authService.logout();
    this.router.navigate(['/']);
  }
  isUser() {
    return this.authService.isLoggedInAsUser();
  }
  isLoggedIn() {
    return this.authService.isLoggedIn();
  }
  isCustomer() {
    return !this.isLoggedIn() || this.isUser();
  }
  isAdmin() {
    return this.authService.isLoggedInAsAdmin();
  }
  isEmployee() {
    return this.authService.isLoggedInAsEmployee();
  }
}
