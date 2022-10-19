import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProducerClient, ProducerCreationRequestModel,} from '../../../api/epharmacy';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-producer',
  templateUrl: './add-producer.component.html',
  styleUrls: ['./add-producer.component.css']
})
export class AddProducerComponent implements OnInit {
  additionForm: FormGroup;
  submitted = false;

  constructor(private formBuilder: FormBuilder,
              private router: Router,
              private producer: ProducerClient) {}

  ngOnInit() {
    this.additionForm = this.formBuilder.group({
      name: ['', Validators.required],
      address: ['', Validators.required]
    });
  }

  onSubmit() {
    this.submitted = true;
    if (this.additionForm.invalid) {
      return;
    }

    const producerCreationRequestModel: ProducerCreationRequestModel = new ProducerCreationRequestModel();
    producerCreationRequestModel.init({
      name: this.f.name.value,
      address: this.f.address.value,

    });
    this.submitted = true;
    this.producer.create(producerCreationRequestModel).subscribe(
      _ => this.router.navigate(['/producer-list']),
      error => {
        console.log('Login error: ', error);
      }
    );
  }

  get f() {
    return this.additionForm.controls;
  }
}
