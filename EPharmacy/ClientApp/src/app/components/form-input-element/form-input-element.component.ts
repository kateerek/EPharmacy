import { Component, Input, forwardRef } from '@angular/core';
import { NG_VALUE_ACCESSOR, ControlValueAccessor, NgModel } from '@angular/forms';

@Component({
  selector: 'app-form-input-element',
  templateUrl: './form-input-element.component.html',
  styleUrls: ['./form-input-element.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => FormInputElementComponent),
      multi: true
    },
  ],
})
export class FormInputElementComponent implements ControlValueAccessor {

  @Input() label: string;
  @Input() name: string;
  @Input() type = 'text';
  @Input() placeholder: string;
  @Input('disabled') isDisabled = false;

  value: string;
  uiValueChangeCallback: (_: any) => void;
  onTouchCallback: () => void;

  writeValue(value: string) {
    this.value = value;
  }

  registerOnChange(fn: (_: any) => void) {
    this.uiValueChangeCallback = fn;
  }

  registerOnTouched(fn: () => void) {
    this.onTouchCallback = fn;
  }

  setDisabledState(isDisabled: boolean) {
    this.isDisabled = isDisabled;
  }

  onUiValueChange(newValue: string) {
    if (this.uiValueChangeCallback) {
      this.uiValueChangeCallback(newValue);
    }
  }

  onTouched() {
    if (this.onTouchCallback) {
      this.onTouchCallback();
    }
  }

}
