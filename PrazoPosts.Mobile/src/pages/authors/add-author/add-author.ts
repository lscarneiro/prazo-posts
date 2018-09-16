import {Component} from '@angular/core';
import {NavController, NavParams, ViewController} from 'ionic-angular';
import {AuthorService} from "../../../app/modules/services/author.service";
import {FormBuilder, FormGroup} from "@angular/forms";

@Component({
  selector: 'modal-add-author',
  templateUrl: 'add-author.html'
})
export class AddAuthor {

  constructor(public navCtrl: NavController,
              private fb: FormBuilder,
              private viewCtrl: ViewController,
              private params: NavParams,
              private authorService: AuthorService) {
    this.formGroup = this.fb.group({
      name: [null],
    });
  }

  formGroup: FormGroup;

  ionViewDidLoad() {

  }

  addAuthor() {

  }

  dismiss() {
    this.viewCtrl.dismiss();
  }
}
