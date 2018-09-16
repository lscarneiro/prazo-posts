import {Component} from '@angular/core';
import {LoadingController, NavController, NavParams, ViewController} from 'ionic-angular';
import {AuthorService} from "../../../app/modules/services/author.service";
import {FormBuilder, FormGroup} from "@angular/forms";
import {FormValidatorService} from "../../../app/modules/core/form-validator.service";
import {Author} from "../../../app/dto/author";

@Component({
  selector: 'modal-add-author',
  templateUrl: 'add-author.html'
})
export class AddAuthor {

  constructor(public navCtrl: NavController,
              private loadingCtrl: LoadingController,
              private fb: FormBuilder,
              private viewCtrl: ViewController,
              private formValidator: FormValidatorService,
              private params: NavParams,
              private authorService: AuthorService) {
    this.formGroup = this.fb.group({
      name: [null],
    });
  }

  formGroup: FormGroup;

  ionViewDidLoad() {

  }

  save() {
    this.formValidator.validate(this.formGroup);
    if (!this.formGroup.valid) {
      return;
    }
    let loading = this.loadingCtrl.create();
    loading.present();
    let author = <Author>Object.assign({}, this.formGroup.value);
    this.authorService.create(author).subscribe(() => {
      loading.dismiss();
      this.dismiss();
    }, () => {
      this.dismiss();
    });
  }

  dismiss() {
    this.viewCtrl.dismiss();
  }
}
