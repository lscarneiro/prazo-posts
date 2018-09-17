import {Component} from '@angular/core';
import {LoadingController, MenuController, NavController} from 'ionic-angular';
import {FormBuilder, FormGroup} from "@angular/forms";
import {FormValidatorService} from "../../app/modules/core/form-validator.service";
import {TokenService} from "../../app/modules/core/token.service";
import {HomePage} from "../home/home";
import {User} from "../../app/dto/user";
import {LoginPage} from "../login/login";
import {UserService} from "../../app/modules/services/user.service";
import {HttpService} from "../../app/modules/core/http.service";
import {ServerValidationMap} from "../../app/modules/core/model/server-validation-map";

@Component({
  selector: 'page-register',
  templateUrl: 'register.html'
})
export class RegisterPage {

  constructor(public navCtrl: NavController,
              private fb: FormBuilder,
              private userService: UserService,
              private formValidator: FormValidatorService,
              private tokenService: TokenService,
              private http: HttpService,
              private loadingCtrl: LoadingController,
              private menuCtrl: MenuController,
  ) {

    this.formGroup = this.fb.group({
      Name: [null],
      Email: [null],
      Password: [null],
      PasswordConfirmation: [null],
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

  serverValidationMap: ServerValidationMap;
  formGroup: FormGroup;

  register(): void {
    this.formValidator.validate(this.formGroup);
    if (!this.formGroup.valid) {
      return;
    }
    let loading = this.loadingCtrl.create();
    loading.present();
    let authData = <User>Object.assign({}, this.formGroup.value);
    this.userService.register(authData)
      .subscribe((tokenData) => {
        if (tokenData) {
          this.tokenService.setAcessToken(tokenData.token);
          loading.dismiss();
          this.goToHome();
        } else {
          loading.dismiss();
        }
      }, (err) => {
        loading.dismiss();
      });
  }


  goToHome() {
    this.navCtrl.setRoot(HomePage);
  }

  goToLogin() {
    this.navCtrl.setRoot(LoginPage);
  }


  public ionViewWillEnter(): void {
    this.menuCtrl.enable(false, 'sideMenu');
  }

  public ionViewDidLeave(): void {
    this.menuCtrl.enable(true, 'sideMenu');
  }
}
