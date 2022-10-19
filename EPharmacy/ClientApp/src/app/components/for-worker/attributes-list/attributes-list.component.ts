import { Component, OnInit } from '@angular/core';
import { ListBase } from '../list-base';
import { AttributeResponseModel, AttributeClient } from '../../../api/epharmacy';

@Component({
  selector: 'app-attributes-list',
  templateUrl: './attributes-list.component.html',
  styleUrls: ['./attributes-list.component.css']
})
export class AttributesListComponent extends ListBase<AttributeResponseModel> implements OnInit {
  selectedAttribute: AttributeResponseModel;

  constructor(private attributeClient: AttributeClient) {
    super();
  }

  ngOnInit() {
    super.getValues(this.attributeClient.getAll());
  }

  updateSelectedAttr(newAttributeValues: any) {
    this.selectedAttribute.init(newAttributeValues);
    this.selectedAttribute = null;
  }

}
