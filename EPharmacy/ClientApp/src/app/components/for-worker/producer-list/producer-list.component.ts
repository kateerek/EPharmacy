import { Component, OnInit} from '@angular/core';
import { ProducerClient, ProducerModel} from '../../../api/epharmacy';
import { ListBase } from '../list-base';


@Component({
  selector: 'app-producer-list',
  templateUrl: './producer-list.component.html',
  styleUrls: ['./producer-list.component.css']
})
export class ProducerListComponent extends ListBase<ProducerModel> implements OnInit {

  selectedProducer: ProducerModel;


  constructor(private producerList: ProducerClient) {
    super();
  }

  ngOnInit() {
    this.getProducers()
  }

  getProducers(){
    super.getValues(this.producerList.getAll());
  }

  onDelete(producer){
    this.producerList.delete(producer.id).subscribe(

      _ =>
        this.getProducers(),
      error => {
        console.log('Login error: ', error);
      }
    );
  }

  onEdit(newProducerValues: any) {
    this.selectedProducer.init(newProducerValues);
    this.selectedProducer = null;
  }

}
