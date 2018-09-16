import {Injectable} from "@angular/core";
import {ToastController} from "ionic-angular";

@Injectable()
export class ToasterService {
  private duration: number = 3000;

  constructor(private toastCtrl: ToastController) {
  }

  showError(message: string): void {
    this.toastCtrl.create({
      message: message,
      duration: this.duration,
      cssClass: 'toast-error'
    }).present();
  }
  showSuccess(message: string): void {
    this.toastCtrl.create({
      message: message,
      duration: this.duration,
      cssClass: 'toast-success'
    }).present();
  }
}
