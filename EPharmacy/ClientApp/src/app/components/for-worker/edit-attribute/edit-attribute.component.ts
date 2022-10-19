import { Component, Input, Output, OnInit, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AttributeClient, AttributeResponseModel, AttributeEditionRequest } from '../../../api/epharmacy';

@Component({
  selector: 'app-edit-attribute',
  templateUrl: './edit-attribute.component.html',
  styleUrls: ['./edit-attribute.component.css']
})
export class EditAttributeComponent implements OnInit {
  @Input() attribute: AttributeResponseModel;
  @Output() attributeChange = new EventEmitter<any>();

  editionForm: FormGroup;
  submitted = false;

  constructor(private formBuilder: FormBuilder,
    private attributeClient: AttributeClient) { }

  ngOnInit() {
    if (this.attribute === null) return;

    this.editionForm = this.formBuilder.group({
      name: [this.attribute.name, Validators.required],
      internalName: [this.attribute.internalName, Validators.compose([Validators.required, Validators.pattern('\\S+')])],
      description: [this.attribute.description],
    });
  }

  onSubmit() {
    this.submitted = true;
    if (this.editionForm.invalid) return;

    let id = this.attribute.id;
    let attributeEditionReq = this.editionForm.value;
    attributeEditionReq['attributeToEditId'] = id;
    
    this.attributeClient.edit(id, AttributeEditionRequest.fromJS(attributeEditionReq))
      .subscribe(() => this.attributeChange.emit(attributeEditionReq));
  }

  get f() { return this.editionForm.controls; }

}
