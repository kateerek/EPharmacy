import { Component, OnInit } from '@angular/core';
import { PrescriptionCategoryInfoModel, DiscountsClient } from '../../api/epharmacy';

@Component({
  selector: 'app-refundation-info',
  templateUrl: './refundation-info.component.html',
  styleUrls: ['./refundation-info.component.css']
})
export class RefundationInfoComponent implements OnInit {

  refundations: PrescriptionCategoryInfoModel[];

  constructor(private discountsClient : DiscountsClient) { }

  ngOnInit() {
    this.updateRefundations();
  }

  updateRefundations() {
    this.discountsClient.getPrescriptionCategories().subscribe(
      refundations => this.refundations = refundations
    );
  }
}
