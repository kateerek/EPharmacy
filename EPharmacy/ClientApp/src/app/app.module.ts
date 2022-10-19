import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { EPharmacyModule } from './api/epharmacy.module';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { FooterComponent } from './footer/footer.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { ProductListComponent } from './components/product-list/product-list.component';
import { FilterTextboxComponent} from './components/product-list/filter-textbox.component';
import { ProductInfoComponent } from './components/product-info/product-info.component';
import { ModifyProductComponent } from './components/modify-product/modify-product.component';


import { DbHandleService } from './services/db-handle/db-handle.service';
import { JwtModule } from '@auth0/angular-jwt';
import { AuthService, getJwtToken } from './services/auth/auth.service';
import { UserDataDetailComponent } from './components/user-data-detail/user-data-detail.component';
import { AuthGuardService } from './services/auth-guard/auth-guard.service';
import { PasswordChangeComponent } from './components/password-change/password-change.component';
import { FormInputElementComponent } from './components/form-input-element/form-input-element.component';
import { ValidationErrorsDisplayComponent } from './components/validation-errors-display/validation-errors-display.component';
import { PasswordValidator } from './directives/password-validator';
import { PlacesComponent } from './components/places/places.component';
import { AgmCoreModule} from "@agm/core";
import { ShoppingCartComponent } from './components/shopping-cart/shopping-cart.component';
import { ShoppingCartService } from './services/shopping/shopping-cart.service';
import { AdminGuardService } from './services/admin-guard/admin-guard.service';
import { EmployeeGuardService } from './services/employee-guard/employee-guard.service';
import { ProdListComponent } from './components/for-worker/prod-list/prod-list.component';
import { ProducerListComponent } from './components/for-worker/producer-list/producer-list.component';
import { AddProductComponent } from './components/for-worker/add-product/add-product.component';
import { ListViewComponent } from './components/for-worker/list-view/list-view.component';
import { ListViewHostDirective } from './components/for-worker/list-view-host.directive';
import { AttributesListComponent } from './components/for-worker/attributes-list/attributes-list.component';
import { AddProducerComponent } from './components/for-worker/add-producer/add-producer.component';
import { LocationListComponent } from './components/for-worker/location-list/location-list.component';
import { AddAttributeComponent } from './components/for-worker/add-attribute/add-attribute.component';
import { OrderListComponent } from './components/order-list/order-list.component';
import { ProductsOfferComponent } from './components/products-offer/products-offer.component';
import { OrderComponent } from './components/order-list/order/order.component';
import { EditAttributeComponent } from './components/for-worker/edit-attribute/edit-attribute.component';
import { ProductTypesListComponent } from './components/for-worker/product-types-list/product-types-list.component';
import { AddProductTypeComponent } from './components/for-worker/add-product-type/add-product-type.component';
import { EditProducerComponent } from './components/for-worker/edit-producer/edit-producer.component';
import { DiscountsListComponent } from './components/for-worker/discounts-list/discounts-list.component';
import { DiscountFormComponent } from './components/for-worker/discount-form/discount-form.component';
import { RefundationInfoComponent } from './components/refundation-info/refundation-info.component';
import { TruncatePipePipe } from './pipe/truncate-pipe.pipe';
import { AddLocationComponent } from './components/for-worker/add-location/add-location.component';
import { EditLocationComponent } from './components/for-worker/edit-location/edit-location.component';
import { RegisterWorkerComponent } from './components/for-admin/register-worker/register-worker.component';
import { EditDiscountComponent } from './components/for-worker/edit-discount/edit-discount.component';
import { AddDiscountComponent } from './components/for-worker/add-discount/add-discount.component';
import { BusinessIntelligenceComponent } from './components/business-intelligence/business-intelligence.component';
import { SalesOrdersListComponent } from './components/for-worker/sales-orders-list/sales-orders-list.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    FooterComponent,
    LoginComponent,
    RegisterComponent,
    PasswordValidator,
    ProductListComponent,
    FilterTextboxComponent,
    ProductInfoComponent,
    ModifyProductComponent,
    UserDataDetailComponent,
    PasswordChangeComponent,
    FormInputElementComponent,
    ValidationErrorsDisplayComponent,
    PlacesComponent,
    ShoppingCartComponent,
    ProdListComponent,
    ProducerListComponent,
    AddProductComponent,
    ListViewComponent,
    ListViewHostDirective,
    AttributesListComponent,
    AddProducerComponent,
    LocationListComponent,
    AddAttributeComponent,
    OrderListComponent,
    ProductsOfferComponent,
    OrderComponent,
    EditAttributeComponent,
    ProductTypesListComponent,
    AddProductTypeComponent,
    EditProducerComponent,
    DiscountsListComponent,
    DiscountFormComponent,
    RefundationInfoComponent,
    TruncatePipePipe,
    AddLocationComponent,
    EditLocationComponent,
    RegisterWorkerComponent,
    EditDiscountComponent,
    AddDiscountComponent,
    BusinessIntelligenceComponent,
    SalesOrdersListComponent
  ],
  entryComponents: [
    ProducerListComponent, ProdListComponent, ProductTypesListComponent, DiscountsListComponent, SalesOrdersListComponent
  ],
  imports: [
    EPharmacyModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    ReactiveFormsModule,
    HttpClientModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: getJwtToken,
        skipWhenExpired: true
      }
    }),
    FormsModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyDIvNHZdNubR34H8HkkdJCk0sz26mgCwAc'
    }),
    RouterModule.forRoot([
      { path: 'home', component: HomeComponent, pathMatch: 'full' },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: '', component: ProductListComponent },
      { path: 'refundation-info', component: RefundationInfoComponent },
      { path: 'product-info/:id', component: ProductInfoComponent },
      { path: 'user', component: UserDataDetailComponent, canActivate: [AuthGuardService] },
      { path: 'password', component: PasswordChangeComponent, canActivate: [AuthGuardService] },
      { path: 'order-list', component: OrderListComponent, canActivate: [AuthGuardService] },
      { path: 'places', component: ListViewComponent, data: { componentType: PlacesComponent } },
      { path: 'shopping-cart', component: ShoppingCartComponent },
      { path: 'add-product', component: AddProductComponent, canActivate: [EmployeeGuardService]},
      { path: 'prod', component: ListViewComponent, canActivate: [EmployeeGuardService], data: { componentType: ProdListComponent } },
      { path: 'producer-list', component: ListViewComponent, canActivate:
        [EmployeeGuardService], data: { componentType: ProducerListComponent } },
      { path: 'add-producer', component: AddProducerComponent, canActivate: [EmployeeGuardService]},
      { path: 'attributes', component: ListViewComponent, canActivate:
        [EmployeeGuardService], data: { componentType: AttributesListComponent }},
      { path: 'location-list', component: ListViewComponent, canActivate:
        [EmployeeGuardService], data: { componentType: LocationListComponent }
      },
      { path: 'add-attribute', component: AddAttributeComponent, canActivate: [EmployeeGuardService] },
      {
        path: 'product-types', component: ListViewComponent, canActivate: [EmployeeGuardService],
        data: { componentType: ProductTypesListComponent }
      },
      { path: 'add-product-type', component: AddProductTypeComponent, canActivate: [EmployeeGuardService] },
      {
        path: 'discounts', component: ListViewComponent, canActivate: [EmployeeGuardService],
        data: { componentType: DiscountsListComponent }
      },
      { path: 'add-discount', component: AddDiscountComponent, canActivate: [EmployeeGuardService] },
      { path: 'add-location', component: AddLocationComponent, canActivate: [EmployeeGuardService]},
      { path: 'register-worker', component: RegisterWorkerComponent, canActivate: [AdminGuardService]},
      { path: 'edit-discount/:id', component: EditDiscountComponent, canActivate: [EmployeeGuardService] },
      { path: 'BI', component: BusinessIntelligenceComponent, canActivate: [AdminGuardService]},
      { path: 'sales-orders-list', component: ListViewComponent, canActivate: [EmployeeGuardService],
        data: { componentType: SalesOrdersListComponent }}

   ])
  ],
  providers: [DbHandleService, AuthService,
              AuthGuardService, ShoppingCartService,
              AdminGuardService, EmployeeGuardService],
  bootstrap: [AppComponent]
})
export class AppModule {}
