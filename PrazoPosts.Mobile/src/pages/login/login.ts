import {Component} from '@angular/core';
import {LoadingController, MenuController, NavController} from 'ionic-angular';
import {FormBuilder, FormGroup} from "@angular/forms";
import {FormValidatorService} from "../../app/modules/core/form-validator.service";
import {TokenService} from "../../app/modules/core/token.service";
import {HomePage} from "../home/home";
import {AuthService} from "../../app/modules/services/auth.service";
import {User} from "../../app/dto/user";
import {RegisterPage} from "../register/register";
import {HttpService} from "../../app/modules/core/http.service";
import {ServerValidationMap} from "../../app/modules/core/model/server-validation-map";

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
              private http: HttpService,
              private menuCtrl: MenuController,
  ) {

    this.formGroup = this.fb.group({
      Email: [null],
      Password: [null],
    });
    this.http.onServerValidationErrors
      .do((r) => this.serverValidationMap = r)
      .delay(1)
      .subscribe(r => {
        this.formValidator.validate(this.formGroup);
      });
  }

  ionViewDidLoad() {

    let token = this.tokenService.getAccessToken();
    if (token) {
      this.goToHome();
    }
  }

  formGroup: FormGroup;
  serverValidationMap: ServerValidationMap;

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
          this.tokenService.setAcessToken(tokenData.Token);
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
    this.navCtrl.setRoot(RegisterPage);
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
