import {Injectable} from '@angular/core';
import {AbstractControl, FormGroup} from '@angular/forms';

@Injectable()
export class FormValidatorService {

  constructor() {
  }

  validate(control: AbstractControl) {
    if (control.hasOwnProperty('controls')) {
      control.markAsTouched();
      let ctrl = <FormGroup>control;
      for (let inner in ctrl.controls) {
        this.validate(ctrl.controls[inner]);

        control.updateValueAndValidity();
      }
    } else {
      control.updateValueAndValidity();
      control.markAsTouched();
    }
  }
}
