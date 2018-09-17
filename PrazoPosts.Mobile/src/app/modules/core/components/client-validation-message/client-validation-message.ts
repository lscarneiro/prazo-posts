import {Component, Input} from "@angular/core";
import {FormGroup} from "@angular/forms";

@Component({
  selector: 'app-client-validation-message',
  templateUrl: 'client-validation-message.html'
})
export class ClientValidationMessage {

  constructor() {
  }


  @Input() formGroup: FormGroup;
  @Input() controlName: string;
  @Input() errorName: string;
  @Input() errorMessage: string;

  hasError(): boolean {
    if (!this.controlName) return false;
    if (!this.formGroup) return false;
    if (!this.formGroup.contains(this.controlName)) return false;
    if (!this.errorName) return false;
    let showError = this.formGroup.get(this.controlName).hasError(this.errorName) && this.formGroup.get(this.controlName).touched;
    return showError;
  }
}
