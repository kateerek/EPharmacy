import { Directive } from '@angular/core';
import { NG_VALIDATORS, Validator, AbstractControl, ValidationErrors } from '@angular/forms';

function hasLowerCaseLetter(value: string): boolean {
  return /.*[a-z].*/.test(value);
}

function hasUpperCaseLetter(value: string): boolean {
  return /.*[A-Z].*/.test(value);
}

function hasDigit(value: string): boolean {
  return /.*[0-9].*/.test(value);
}

function hasNonAlphanumeric(value: string): boolean {
  return /.*[^a-zA-Z0-9].*/.test(value);
}

@Directive({
  selector: '[password]',
  providers: [{ provide: NG_VALIDATORS, useExisting: PasswordValidator, multi: true }]
})
export class PasswordValidator implements Validator {
  validate(control: AbstractControl): ValidationErrors | null {
    let password: string;
    if (control.value === null || control.value === undefined) {
      password = '';
    }
    else {
      password = control.value;
    }
    
    let errors = {};
    if (password.length < 8)
      errors['tooShort'] = true;
    if (!hasLowerCaseLetter(password))
      errors['noLowerCase'] = true;
    if (!hasUpperCaseLetter(password))
      errors['noUpperCase'] = true;
    if (!hasDigit(password))
      errors['noDigit'] = true;
    if (!hasNonAlphanumeric(password))
      errors['noNonAlphanumeric'] = true;
    return Object.keys(errors).length === 0 ? null : errors;
  }
}

export function getPasswordValidationErrorMessage(errorType: string) {
  switch (errorType) {
    case 'tooShort':          return 'Password must be at least 8 characters long';
    case 'noLowerCase':       return 'At least one lower case letter is required';
    case 'noUpperCase':       return 'At least one upper case letter is required';
    case 'noDigit':           return 'At least one digit is required';
    case 'noNonAlphanumeric': return 'At least one non-alphanumeric character is required';
  }
}
