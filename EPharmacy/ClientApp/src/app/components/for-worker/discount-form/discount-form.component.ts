import { Component, OnInit, Output, Input, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ValidatorFn, ValidationErrors } from '@angular/forms';
import { AttributeClient, AttributeResponseModel, ProductClient, ProductDetailsListModel, BadRequestResponse } from '../../../api/epharmacy';

function nothingSelectedValidator(component): ValidatorFn {
  return (_: FormGroup): ValidationErrors | null => {
    if (component.selectedProducts.length == 0 && component.selectedAttributes.length == 0)
      return { noElementsSelected: true };
    return null;
  };
}

@Component({
  selector: 'app-discount-form',
  templateUrl: './discount-form.component.html',
  styleUrls: ['./discount-form.component.css']
})
export class DiscountFormComponent implements OnInit {
  @Input() name: string;
  @Input() validTo: Date | null = null;
  @Input() description: string;
  @Input() discountType: number | null = null;
  discountValueStr: string;
  @Input('discountValue') set discountValue(value) { this.discountValueStr = '' + value; }

  @Input('products') selectedProducts: ProductDetailsListModel[] = [];
  @Input('attributes') selectedAttributes: AttributeResponseModel[] = [];

  @Input() submitBtnText: string = 'Dodaj';

  @Output('submition') submitEvent = new EventEmitter<object>();

  form: FormGroup;
  submitted = false;

  errors: BadRequestResponse;
  objectValues = Object.values;

  attributesList: AttributeResponseModel[];
  selectedAttributeId: number | null = null;

  productsList: ProductDetailsListModel[];

  constructor(private formBuilder: FormBuilder,
    private attributes: AttributeClient,
    private products: ProductClient) { }

  ngOnInit() {
    this.attributes.getAll().subscribe(attrs => {
      this.attributesList = attrs;
      this.form = this.formBuilder.group({
        name: ['', Validators.required],
        validTo: ['', Validators.required],
        description: [''],
        discountType: ['', Validators.required],
        discountValue: ['', Validators.compose([Validators.required, Validators.pattern(/^\d+([.,]\d*)?$/)])],
        product: [''],
        attribute: [attrs[0].name],
      }, { validator: nothingSelectedValidator(this) });
    });
  }

  xxx(obj) {
    console.log(obj);
  }

  onSubmit() {
    this.submitted = true;
    if (this.form.invalid) return;

    const discountModel = {
      name: this.name,
      validTo: this.validTo,
      description: this.description,
      discountValue: {
        discountType: this.discountType,
        value: +this.discountValueStr.replace(',', '.')
      },
      products: [],
      attributes: []
    };
    this.selectedProducts.forEach(prod => discountModel.products.push(prod.id));
    this.selectedAttributes.forEach(attr => discountModel.attributes.push(attr.id));

    this.submitEvent.emit(discountModel);
  }

  convertStringToDate(date: string): Date | null {
    if (date == null) return null;

    const elems = date.split('-');
    const year = +elems[0];
    const month = +elems[1] - 1;
    const day = +elems[2];

    if (year < 1970) return null;
    return new Date(year, month, day);
  }

  onSelectedAttributeChanged(attributeName: string) {
    this.productsList = [];
    let attributeId = this.attributesList.find(attr => attr.name == attributeName).id;
    this.selectedAttributeId = attributeId;
    this.products.productsByAttributes([attributeId]).subscribe(products => {
      this.productsList = products;
      this.form.controls['product'].setValue(products && products.length > 0 && products[0].name);
    });
  }

  addProduct() {
    let productName = this.form.controls['product'].value;
    if (!productName) return;

    let product = this.productsList.find(prod => prod.name == productName);
    if (this.selectedProducts.find(prod => prod.id == product.id) === undefined) {
      this.selectedProducts.push(product);
    }

    this.form.updateValueAndValidity({ onlySelf: true });
  }

  removeProduct(product: ProductDetailsListModel) {
    this.selectedProducts = this.selectedProducts.filter(prod => prod.id !== product.id);
    this.form.updateValueAndValidity({ onlySelf: true });
  }

  addAttribute() {
    let attributeName = this.form.controls['attribute'].value;
    if (!attributeName) return;

    let attribute = this.attributesList.find(attr => attr.name == attributeName);
    if (this.selectedAttributes.find(attr => attr.id == attribute.id) === undefined) {
      this.selectedAttributes.push(attribute);
    }

    this.form.updateValueAndValidity({ onlySelf: true });
  }

  removeAttribute(attribute: AttributeResponseModel) {
    this.selectedAttributes = this.selectedAttributes.filter(attr => attr.id !== attribute.id);
    this.form.updateValueAndValidity({ onlySelf: true });
  }

  get f() { return this.form.controls; }

}
