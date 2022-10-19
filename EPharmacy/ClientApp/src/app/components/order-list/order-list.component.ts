import { Component, OnInit, OnDestroy } from '@angular/core';
import { SalesOrderClient, SalesOrderResponse } from '../../api/epharmacy';
import { Subscription } from 'rxjs/Subscription';

@Component({
  selector: 'app-order-list',
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.css']
})
export class OrderListComponent implements OnInit, OnDestroy {

  list: SalesOrderResponse[];
  oderList: Subscription;
  constructor(private orders: SalesOrderClient) { }

  ngOnInit() {
    this.oderList = this.orders.getForUser().subscribe( element => {
      this.list = element;
    });
  }
  ngOnDestroy() {
    this.oderList.unsubscribe();
  }
}
