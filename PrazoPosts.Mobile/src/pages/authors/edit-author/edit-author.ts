import {Component} from '@angular/core';
import {LoadingController, NavController, NavParams, ViewController} from 'ionic-angular';
import {AuthorService} from "../../../app/modules/services/author.service";
import {FormBuilder, FormGroup} from "@angular/forms";
import {FormValidatorService} from "../../../app/modules/core/form-validator.service";
import {Author} from "../../../app/dto/author";
import {ToasterService} from "../../../app/modules/core/toaster.service";
import {ServerValidationMap} from "../../../app/modules/core/model/server-validation-map";
import {HttpService} from "../../../app/modules/core/http.service";

@Component({
  selector: 'modal-add-author',
  templateUrl: 'edit-author.html'
})
export class EditAuthor {

  constructor(public navCtrl: NavController,
              private loadingCtrl: LoadingController,
              private fb: FormBuilder,
              private viewCtrl: ViewController,
              private formValidator: FormValidatorService,
              private toastSvc: ToasterService,
              private http: HttpService,
              private params: NavParams,
              private authorService: AuthorService) {
    this.formGroup = this.fb.group({
      Name: [null],
    });
    this.http.onServerValidationErrors
      .do((r) => this.serverValidationMap = r)
      .delay(1)
      .subscribe(r => {
        this.formValidator.validate(this.formGroup);
      });
    this.author = this.params.get('author');
    if (this.author) {
      this.formGroup.patchValue(this.author);
      this.title = "Alterar";
    }
  }

  author: Author;
  title: string = "Novo";
  formGroup: FormGroup;
  serverValidationMap: ServerValidationMap;

  ionViewDidLoad() {
  }

  save() {
    this.formValidator.validate(this.formGroup);
    if (!this.formGroup.valid) {
      return;
    }
    let loading = this.loadingCtrl.create();
    loading.present();
    let author = <Author>Object.assign(this.author || {}, this.formGroup.value);
    if (author.Id) {
      this.authorService.update(author).subscribe(() => {
        loading.dismiss();
        this.dismiss();
      }, () => {
        this.toastSvc.showError("Não foi possível realizar a operação");
        loading.dismiss();
      });
    } else {
      this.authorService.create(author).subscribe(() => {
        loading.dismiss();
        this.dismiss();
      }, () => {
        this.toastSvc.showError("Não foi possível realizar a operação");
        loading.dismiss();
      });
    }
  }

  dismiss() {
    this.viewCtrl.dismiss();
  }
}
