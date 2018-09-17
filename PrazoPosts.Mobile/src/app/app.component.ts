import {Component} from '@angular/core';
import {App, MenuController, Platform} from 'ionic-angular';
import {StatusBar} from '@ionic-native/status-bar';
import {SplashScreen} from '@ionic-native/splash-screen';

import {LoginPage} from "../pages/login/login";
import {TokenService} from "./modules/core/token.service";
import {UserService} from "./modules/services/user.service";
import {HomePage} from "../pages/home/home";
import {AuthorsPage} from "../pages/authors/authors";

@Component({
  templateUrl: 'app.html'
})
export class MyApp {
  rootPage: any = LoginPage;
  userName: string = null;

  constructor(platform: Platform, statusBar: StatusBar, splashScreen: SplashScreen,
              private tokenService: TokenService,
              private app: App,
              private userService: UserService,
              private menuCtrl: MenuController) {
    platform.ready().then(() => {
      // Okay, so the platform is ready and our plugins are available.
      // Here you can do any higher level native things you might need.
      statusBar.styleDefault();
      splashScreen.hide();
    });
    if (tokenService.getAccessToken() !== null) {
      this.getData();
    }
  }

  getData(): void {
    this.userService.get()
      .filter(x => !!x)
      .subscribe(userData => {
        this.userName = userData.Name;
      });
  }

  goToHome() {
    this.menuCtrl.close();
    this.app.getRootNavs()[0].setRoot(HomePage);
  }

  goToAuthors() {
    this.menuCtrl.close();
    this.app.getRootNavs()[0].setRoot(AuthorsPage);
  }

  private goToLogin() {
    this.tokenService.clear();
    this.userService.clearCache();
    this.rootPage = LoginPage;
    this.app.getRootNavs()[0].setRoot(LoginPage);
  }

  logout() {
    this.menuCtrl.close();
    this.goToLogin();
  }
}

