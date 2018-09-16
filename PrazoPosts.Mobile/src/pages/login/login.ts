import {Component} from '@angular/core';
import {LoadingController, MenuController, NavController} from 'ionic-angular';
import {FormBuilder, FormGroup} from "@angular/forms";
import {FormValidatorService} from "../../app/modules/core/form-validator.service";
import {TokenService} from "../../app/modules/core/token.service";
import {HomePage} from "../home/home";
import {AuthService} from "../../app/modules/services/auth.service";
import {User} from "../../app/dto/user";

@Component({
  selector: 'page-login',
  templateUrl: 'login.html'
})
export class LoginPage {

  constructor(public navCtrl: NavController,
              private fb: FormBuilder,
              private authService: AuthService,
              private formValidator: FormValidatorService,
              private tokenService: TokenService,
              private loadingCtrl: LoadingController,
              private menuCtrl: MenuController,
  ) {

    this.formGroup = this.fb.group({
      email: [null],
      password: [null],
    });
  }

  ionViewDidLoad() {

    let token = this.tokenService.getAccessToken();
    if (token) {
      this.goToHome();
    }
  }

  formGroup: FormGroup;

  login(): void {
    this.formValidator.validate(this.formGroup);
    if (!this.formGroup.valid) {
      return;
    }
    let loading = this.loadingCtrl.create();
    loading.present();
    let authData = <User>Object.assign({}, this.formGroup.value);
    this.authService.authenticate(authData)
      .subscribe((tokenData) => {
        if (tokenData) {
          this.tokenService.setAcessToken(tokenData.token);
          loading.dismiss();
          this.goToHome();
        } else {
          loading.dismiss();
        }
      }, () => {
        loading.dismiss();
      });
  }


  signUp() {
  }

  goToHome() {
    this.navCtrl.setRoot(HomePage);
  }


  public ionViewWillEnter(): void {
    this.menuCtrl.enable(false, 'sideMenu');
  }

  public ionViewDidLeave(): void {
    this.menuCtrl.enable(true, 'sideMenu');
  }
}
