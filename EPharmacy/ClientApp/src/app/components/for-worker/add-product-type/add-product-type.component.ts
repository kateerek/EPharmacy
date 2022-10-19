import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ProductTypeCreationRequest, ProductClient } from '../../../api/epharmacy';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-product-type',
  templateUrl: './add-product-type.component.html',
  styleUrls: ['./add-product-type.component.css']
})
export class AddProductTypeComponent implements OnInit {

  additionForm: FormGroup;
  submitted = false;

  constructor(private formBuilder: FormBuilder,
    private productClient: ProductClient,
    private router: Router) { }

  ngOnInit() {
    this.additionForm = this.formBuilder.group({
      name: ['', Validators.required],
      internalName: ['', Validators.compose([Validators.required, Validators.pattern('\\S+')])],
    });
  }

  onSubmit() {
    this.submitted = true;
    if (this.additionForm.invalid) return;

    this.productClient.createProductType(ProductTypeCreationRequest.fromJS(this.additionForm.value)).subscribe(
      () => this.router.navigate(['/product-types'])
    );
  }

  get f() { return this.additionForm.controls; }

}
