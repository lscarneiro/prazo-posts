import {BrowserModule} from '@angular/platform-browser';
import {ErrorHandler, NgModule} from '@angular/core';
import {IonicApp, IonicErrorHandler, IonicModule} from 'ionic-angular';
import {SplashScreen} from '@ionic-native/splash-screen';
import {StatusBar} from '@ionic-native/status-bar';

import {MyApp} from './app.component';
import {MODULES_PROVIDERS} from "./modules";
import {PAGES} from "../pages";
import {HttpClientModule} from "@angular/common/http";
import {CORE_COMPONENTS} from "./modules/core/components";
import {CORE_CDIRECTIVES} from "./modules/core/directives";

@NgModule({
  declarations: [
    MyApp,
    ...PAGES,
    ...CORE_COMPONENTS,
    ...CORE_CDIRECTIVES
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    IonicModule.forRoot(MyApp)
  ],
  bootstrap: [IonicApp],
  entryComponents: [
    MyApp,
    ...PAGES
  ],
  providers: [
    StatusBar,
    SplashScreen,
    ...MODULES_PROVIDERS,
    {provide: ErrorHandler, useClass: IonicErrorHandler}
  ]
})
export class AppModule {
}
