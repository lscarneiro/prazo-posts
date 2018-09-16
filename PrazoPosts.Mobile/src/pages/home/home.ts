import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import {Post} from "../../app/dto/post";

@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})
export class HomePage {

  posts: Post[] = [];
  constructor(public navCtrl: NavController) {

  }
  ionViewDidLoad() {

  }
}
