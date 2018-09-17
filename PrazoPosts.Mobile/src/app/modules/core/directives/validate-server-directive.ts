import {Directive, forwardRef, Input} from '@angular/core';
import {AbstractControl, FormGroup, NG_VALIDATORS, Validator} from '@angular/forms';
import {ServerValidationMap} from "../model/server-validation-map";

@Directive({
  selector: '[appValidateServer]',
  providers: [
    {provide: NG_VALIDATORS, useExisting: forwardRef(() => ValidateServerDirective), multi: true}
  ]
})
export class ValidateServerDirective implements Validator {
  constructor() {
  }

  @Input('appValidateServer') serverMessageMap: ServerValidationMap;
  @Input('formControlName') formControlName: string;

  validate(control: AbstractControl): { [key: string]: any; } {
    if (!this.serverMessageMap) {
      return null;
    }
    if (!control.parent) {
      return null;
    }
    let controlName = this.formControlName;
    if (control.hasOwnProperty('controls')) {
      let ctrl = <FormGroup>control;
      for (let name in ctrl.controls) {
        if (control === ctrl.controls[name]) {
          controlName = name;
          break;
        }
      }
    }
    if (!controlName) {
      return null;
    }
    let errorMessage = null;
    if (this.serverMessageMap.hasOwnProperty(controlName)) {
      errorMessage = this.serverMessageMap[controlName];
    } else {
      return null;
    }

    if (errorMessage) {
      control.valueChanges.take(1).subscribe(() => this.serverMessageMap[controlName] = null);
    }

    return errorMessage ? {
      server: {
        valid: false,
        message: errorMessage
      }
    } : null;
  }
}
