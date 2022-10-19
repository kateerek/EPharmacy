import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';

import { AccountClient, AttributeClient, ProductClient,
         PharmacyClient, SalesOrderClient, ProducerClient,
         DiscountsClient, StorageClient, ActiveSubstanceClient, BusinessIntelligenceClient } from './epharmacy';


@NgModule({
  imports: [
    HttpModule
  ],
  providers: [
    AccountClient,
    AttributeClient,
    ProductClient,
    PharmacyClient,
    SalesOrderClient,
    ProducerClient,
    DiscountsClient,
    StorageClient,
    ActiveSubstanceClient,
    BusinessIntelligenceClient
  ]
})
export class EPharmacyModule {
}
