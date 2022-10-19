import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AttributeClient, AttributeCreationRequest } from '../../../api/epharmacy';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-attribute',
  templateUrl: './add-attribute.component.html',
  styleUrls: ['./add-attribute.component.css']
})
export class AddAttributeComponent implements OnInit {
  additionForm: FormGroup;
  submitted = false;

  constructor(private formBuilder: FormBuilder,
    private attributeClient: AttributeClient,
    private router: Router) { }

  ngOnInit() {
    this.additionForm = this.formBuilder.group({
      name: ['', Validators.required],
      internalName: ['', Validators.compose([Validators.required, Validators.pattern('\\S+')])],
      description: [''],
    });
  }

  onSubmit() {
    this.submitted = true;
    if (this.additionForm.invalid) return;

    this.attributeClient.create(AttributeCreationRequest.fromJS(this.additionForm.value)).subscribe(
      () => this.router.navigate(['/attributes'])
    );
  }

  get f() { return this.additionForm.controls; }

}
