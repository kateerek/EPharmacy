import { Component, OnInit, Input } from '@angular/core';
import { NgModel, ValidationErrors } from '@angular/forms';

@Component({
  selector: 'app-validation-errors-display',
  templateUrl: './validation-errors-display.component.html',
  styleUrls: ['./validation-errors-display.component.css']
})
export class ValidationErrorsDisplayComponent implements OnInit {

  @Input() model: NgModel;
  objectKeys = Object.keys;
  @Input('messages-factory') errorMessagesFactory: (_: string) => string;
  @Input('message') defaultErrorMsg = '';

  @Input('show-on-touched') showOnTouched = true;
  @Input('show-on-dirty') showOnDirty = false;

  constructor() { }

  ngOnInit() {
  }

  getErrorMessage(errorType: string) {
    if (this.errorMessagesFactory) {
      return this.errorMessagesFactory(errorType);
    }
    return this.defaultErrorMsg;
  }

  isTouchConditionMet() {
    return this.showOnTouched ? this.model.touched : true;
  }

  isDirtyConditionMet() {
    return this.showOnDirty ? this.model.dirty : true;
  }

  isVisible() {
    return this.model.errors != null && this.isTouchConditionMet() && this.isDirtyConditionMet();
  }

}
