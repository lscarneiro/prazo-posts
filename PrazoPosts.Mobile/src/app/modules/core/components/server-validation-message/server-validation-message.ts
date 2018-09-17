import {Component, Input} from "@angular/core";
import {FormGroup} from "@angular/forms";

@Component({
  selector: 'app-server-validation-message',
  templateUrl: 'server-validation-message.html'
})
export class ServerValidationMessage {

  constructor() {

  }

  @Input() controlName: string;
  @Input() formGroup: FormGroup;
  @Input() map: any;


  errorMessage: string;

  hasError(): boolean {
    if (!this.controlName) return false;
    if (!this.formGroup) return false;
    if (!this.formGroup.contains(this.controlName)) return false;
    if (!this.formGroup.get(this.controlName).hasError('server')) return false;
    if (!this.formGroup.get(this.controlName).touched) return false;
    this.errorMessage = this.formGroup.get(this.controlName).getError('server').message;
    return true;
  }
}
