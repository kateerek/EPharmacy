import { Component, OnInit, ViewChild } from '@angular/core';
import {
  DiscountsClient, DiscountDetailsResponse, ProductDetailsListModel,
  ProductClient, AttributeClient, AttributeResponseModel, UpdateOfferRequest
} from '../../../api/epharmacy';
import { ActivatedRoute, Router } from '@angular/router';
import { DiscountFormComponent } from '../discount-form/discount-form.component';

@Component({
  selector: 'app-edit-discount',
  templateUrl: './edit-discount.component.html',
  styleUrls: ['./edit-discount.component.css']
})
export class EditDiscountComponent implements OnInit {
  @ViewChild(DiscountFormComponent)
  private discountFormComponent: DiscountFormComponent;

  discountModel: DiscountDetailsResponse;

  productsList: ProductDetailsListModel[] = [];
  attributesList: AttributeResponseModel[] = [];

  constructor(private route: ActivatedRoute,
    private router: Router,
    private discounts: DiscountsClient,
    private products: ProductClient,
    private attributes: AttributeClient) { }

  ngOnInit() {
    this.route.paramMap.subscribe(params => this.fetchDiscountDetailAsync(+params.get('id')));
    this.products.getDetailsForProducts([1, 2]).subscribe();
  }

  fetchDiscountDetailAsync(id: number) {
    this.discounts.getOfferDetails(id).subscribe(
      discount => {
        this.discountModel = discount;
        this.products.getDetailsForProducts(discount.products).subscribe(
          productsList => this.productsList = productsList
        );
        this.attributes.getAttributesDetails(discount.attributes).subscribe(
          attributesList => this.attributesList = attributesList
        );
      }
    );
  }

  onSubmit(discountModel) {
    let request = UpdateOfferRequest.fromJS(discountModel);
    request.id = this.discountModel.discount.id;
    this.discounts.updateOffer(request).subscribe(
      () => this.router.navigate(['/discounts']),
      error => this.discountFormComponent.errors = error
    );
  }

}
