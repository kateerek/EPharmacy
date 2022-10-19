import { Component, Input, Output, OnInit, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import {ProducerClient, ProducerModel} from '../../../api/epharmacy';

@Component({
  selector: 'app-edit-producer',
  templateUrl: './edit-producer.component.html',
  styleUrls: ['./edit-producer.component.css']
})
export class EditProducerComponent implements OnInit {

  @Input() producer: ProducerModel;
  @Output() producerChange = new EventEmitter<any>();

  editionForm: FormGroup;
  submitted = false;

  constructor(private formBuilder: FormBuilder,
              private producerClient: ProducerClient) { }

  ngOnInit() {
    if (this.producer === null) return;

    this.editionForm = this.formBuilder.group({
      name: [this.producer.name, Validators.required],
      address: [this.producer.address, Validators.required],
    });
  }

  onSubmit() {
    this.submitted = true;
    if (this.editionForm.invalid) return;

    let id = this.producer.id;
    let producerModel = this.editionForm.value;
    producerModel['id'] = id;

    this.producerClient.edit(id, ProducerModel.fromJS(producerModel))
      .subscribe(() => this.producerChange.emit(producerModel));
  }

  get f() { return this.editionForm.controls; }


}
