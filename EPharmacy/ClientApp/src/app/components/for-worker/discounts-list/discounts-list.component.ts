import { Component, OnInit } from '@angular/core';
import { ListBase } from '../list-base';
import { DiscountInfoModel, DiscountsClient } from '../../../api/epharmacy';

@Component({
  selector: 'app-discounts-list',
  templateUrl: './discounts-list.component.html',
  styleUrls: ['./discounts-list.component.css']
})
export class DiscountsListComponent extends ListBase<DiscountInfoModel> implements OnInit {

  constructor(private discounts: DiscountsClient) {
    super();
  }

  ngOnInit() {
    super.getValues(this.discounts.getActiveOffers());
  }

  deleteDiscount(id: number) {
    this.discounts.deleteOffer(id).subscribe(
      () => {
        this.values = this.values.filter(discount => discount.id !== id);
        this.setPage(this.page);
      }
    );
  }

}
