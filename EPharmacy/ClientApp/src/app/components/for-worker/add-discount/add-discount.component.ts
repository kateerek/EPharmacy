import { Component, OnInit, ViewChild } from '@angular/core';
import { DiscountsClient, CreateOfferRequest } from '../../../api/epharmacy';
import { Router } from '@angular/router';
import { DiscountFormComponent } from '../discount-form/discount-form.component';

@Component({
  selector: 'app-add-discount',
  templateUrl: './add-discount.component.html',
  styleUrls: ['./add-discount.component.css']
})
export class AddDiscountComponent implements OnInit {
  @ViewChild(DiscountFormComponent)
  private discountFormComponent: DiscountFormComponent;

  constructor(private discounts: DiscountsClient, private router: Router) { }

  ngOnInit() {
  }

  onSubmit(discountModel: object) {
    this.discounts.createOffer(CreateOfferRequest.fromJS(discountModel)).subscribe(
      () => this.router.navigate(['/discounts']),
      error => this.discountFormComponent.errors = error
    );
  }

}
