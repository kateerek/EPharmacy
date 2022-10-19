import { Component, OnInit } from '@angular/core';
import { ListBase } from '../list-base';
import { SalesOrderResponse, SalesOrderClient } from '../../../api/epharmacy';

@Component({
  selector: 'app-sales-orders-list',
  templateUrl: './sales-orders-list.component.html',
  styleUrls: ['./sales-orders-list.component.css']
})
export class SalesOrdersListComponent extends ListBase<SalesOrderResponse> implements OnInit {
  selectedOrder: SalesOrderResponse;
  selectedTabIndex = 0;

  orderIdInputValue: string;

  errorMessage: string;

  constructor(private salesOrders: SalesOrderClient) {
    super();
  }

  ngOnInit() {
    super.getValues(this.salesOrders.getInProgress());
  }

  changeSelection(order: SalesOrderResponse) {
    this.selectedOrder = order;
  }

  toggleTab(index: number) {
    if (this.selectedTabIndex !== index) {
      this.selectedTabIndex = index;
      this.fetchNewValues();
    }
  }

  fetchNewValues() {
    this.selectedOrder = null;
    if (this.selectedTabIndex === 0) {
      this.salesOrders.getInProgress().subscribe(orders => {
        this.values = orders;
        this.setPage(1);
      });
    }
    else if (this.selectedTabIndex === 1) {
      this.salesOrders.getCompleted().subscribe(orders => {
        this.values = orders;
        this.setPage(1);
      });
    }
  }

  markOrderAsCompleted(orderId: number) {
    this.salesOrders.setAsCompleted(orderId).subscribe(
      () => {
        this.updateValues(orderId);
        this.setPage(this.page);
        this.errorMessage = null;
      },
      error => this.errorMessage = error.message
    );
  }

  updateValues(completedOrderId: number) {
    if (this.selectedTabIndex === 0) {
      this.values = this.values.filter(order => order.id !== completedOrderId);
      this.setPage(this.page);
    }
    else if (this.selectedTabIndex === 1) {
      this.fetchNewValues();
    }
  }

  isValidNumber(input: string) {
    if (input == null)
      return false;
    if (input.length === 0)
      return false;
    
    return /^\d+$/.test(input);
  }

}
