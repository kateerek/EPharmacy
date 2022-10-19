import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProducerClient, ProducerModel, ProductClient, ProductTypeModel,
         AttributeClient, AttributeResponseModel, ProductCreationRequest,
         ProductAttributeInformationModel, IProductAttributeInformationModel,
         ActiveSubstanceClient,
         ActiveSubstanceResponse,
         PrescriptionCategoryInfoModel,
         DiscountsClient,
         ProductActiveSubstanceModel,
         PrescriptionDiscountModel} from '../../../api/epharmacy';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';
@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent implements OnInit {
  attributes: Observable<AttributeResponseModel[]>;
  activeAttributes: AttributeResponseModel[] = [];
  substances: Observable<ActiveSubstanceResponse[]>;
  activePrescriptions: Prescription[]  = [];
  prescriptions: Observable<PrescriptionCategoryInfoModel[]>;
  activeSubstances: Substances[]  = [];
  types: Observable<ProductTypeModel[]>;
  producers: Observable<ProducerModel[]>;
  additionForm: FormGroup;
  submitted = false;
  pricePattern = '^\\d+\\.\\d{0,2}$';
  urlPattern = '(https?://)?([\\da-z.-]+)\\.([a-z.]{2,6})[/\\w .-]*/?';
  constructor(private formBuilder: FormBuilder,
              private router: Router,
              private producer: ProducerClient,
              private product: ProductClient,
              private attribute: AttributeClient,
              private subst: ActiveSubstanceClient,
              private discount: DiscountsClient ) {   }

  ngOnInit() {
    this.prescriptions = this.discount.getPrescriptionCategories();
    this.substances = this.subst.getAll();
    this.producers = this.producer.getAll();
    this.types = this.product.getAllProductTypes();
    this.attributes =  this.attribute.getAll();
    this.additionForm = this.formBuilder.group({
      name: ['', Validators.required],
      price: ['' , [Validators.required, Validators.pattern(this.pricePattern) ]],
      recommendedIntake: ['', Validators.required],
      composition: ['', Validators.required],
      indicationForUse: ['', Validators.required],
      imageUrl: ['',  [Validators.required, Validators.pattern(this.urlPattern) ]],
      producer: ['', Validators.required],
      type: ['', Validators.required],
      attributes: ['', Validators.required],
      substances: ['', Validators.required],
      prescription: ['', Validators.required],
      prescript: ['']
    });
  }
  addAttribute()  {
    const index = this.activeAttributes.findIndex(element => element.id === this.f.attributes.value.id );
   if (index === -1) {
    this.activeAttributes.push(this.f.attributes.value);
   }

  }
  removeAttribute(value) {
   const index = this.activeAttributes.findIndex(element => element.id === value.id );
   if (index !== -1) {
    this.activeAttributes.splice(index, 1);
   }
  }
  addSubstance()  {
    const index = this.activeSubstances.findIndex(element => element.item.id === this.f.substances.value.id );
   if (index === -1) {
    this.activeSubstances.push({item: this.f.substances.value, value: 0});
   }

  }
  removeSubstance(value) {
   const index = this.activeSubstances.findIndex(element => element.item.id === value.item.id );
   if (index !== -1) {
    this.activeSubstances.splice(index, 1);
   }
  }
  addPrescription()  {
    const index = this.activePrescriptions.findIndex(element => element.item.id === this.f.prescription.value.id );
   if (index === -1) {
    this.activePrescriptions.push({item: this.f.prescription.value, value: 0});
   }

  }
  removePrescription(value) {

   const index = this.activePrescriptions.findIndex(element => element.item.id === value.item.id );
   if (index !== -1) {
    this.activePrescriptions.splice(index, 1);
   }
  }

  get f() {
    return this.additionForm.controls;
  }

  onSubmit() {
    console.log(this.f.prescript.value);
    this.submitted = true;
    if (this.additionForm.invalid || this.activeAttributes.length === 0) {
      return;
    }

const prod: ProductCreationRequest = new ProductCreationRequest();
prod.init({
  name: this.f.name.value,
  productPrice: Number(this.f.price.value),
  productInformation: {
    recommendedIntake: this.f.recommendedIntake.value,
    composition: this.f.composition.value,
    indicationForUse: this.f.indicationForUse.value,
  },
  producerId: this.f.producer.value,
  productTypeId: this.f.type.value,
  imageUrl: this.f.imageUrl.value,
  prescriptionInformationId: 0,
  attributes: this.setAttributes(),
  productActiveSubstance: [],
  prescriptionDiscount: []
  });
  if (this.f.prescript.value === true) {
    prod.prescriptionInformationId = 1;
  }
  prod.productActiveSubstances = this.createSubstanceArray();
  prod.prescriptionDiscounts = this.createPrescriptionArray();
  console.log(prod);
    this.submitted = true;
    this.product.create(prod).subscribe(
      _ => this.router.navigate(['/']),
      error => {
        console.log('Login error: ', error);
      }
    );
  }
  createSubstanceArray(): ProductActiveSubstanceModel[] {
    const array: ProductActiveSubstanceModel[] = [];
    this.activeSubstances.forEach(element => {
      let elem: ProductActiveSubstanceModel = new ProductActiveSubstanceModel();
      elem.init({ activeSubstanceId: element.item.id, amount: Number(element.value) });
      array.push(elem);
    });
    return  array;
  }
    setAttributes(): ProductAttributeInformationModel[] {
    const returnAttributes: ProductAttributeInformationModel[] = [];
    this.activeAttributes.forEach(element => {
    const attributeElement: IProductAttributeInformationModel = {attributeId: element.id, isActive: true} ;
      returnAttributes.push(new ProductAttributeInformationModel(attributeElement));
    });
    return returnAttributes;
  }
  createPrescriptionArray(): PrescriptionDiscountModel[] {
  const array: PrescriptionDiscountModel[] = [];
  this.activePrescriptions.forEach(element => {
    let prescriptionElement: PrescriptionDiscountModel = new PrescriptionDiscountModel();
    if (element.item.id === 11 || element.item.id === 12 || element.item.id === 3
       || element.item.id === 14 || element.item.id === 15) {
      prescriptionElement.init({prescriptionCategoryId: element.item.id, discountValue: { discountType: 0, value: Number(element.value)}});
    } else {
      prescriptionElement.init({prescriptionCategoryId: element.item.id, discountValue: { discountType: 1, value: Number(element.value)}});
    }
    array.push(prescriptionElement);
  });
  return array;
  }
}
interface Prescription {
  item: PrescriptionCategoryInfoModel;
  value: number;
}
interface Substances {
  item: ActiveSubstanceResponse;
  value: number;
}
