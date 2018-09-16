import {Component} from '@angular/core';
import {LoadingController, NavController, NavParams, ViewController} from 'ionic-angular';
import {AuthorService} from "../../../app/modules/services/author.service";
import {FormBuilder, FormGroup} from "@angular/forms";
import {FormValidatorService} from "../../../app/modules/core/form-validator.service";
import {ToasterService} from "../../../app/modules/core/toaster.service";
import {Post} from "../../../app/dto/post";
import {PostService} from "../../../app/modules/services/post.service";
import {Author} from "../../../app/dto/author";

@Component({
  selector: 'modal-add-post',
  templateUrl: 'edit-post.html'
})
export class EditPost {

  constructor(public navCtrl: NavController,
              private loadingCtrl: LoadingController,
              private fb: FormBuilder,
              private viewCtrl: ViewController,
              private formValidator: FormValidatorService,
              private toastSvc: ToasterService,
              private params: NavParams,
              private authorService: AuthorService,
              private postService: PostService) {
    this.formGroup = this.fb.group({
      title: [null],
      authorId: [null],
      content: [null],
    });
    this.post = this.params.get('post');
    if (this.post) {
      this.formGroup.patchValue(this.post);
      this.title = "Alterar";
    }
    this.loadAuthors();
  }

  post: Post = null;
  authors: Author[];
  title: string = "Novo";
  formGroup: FormGroup;

  ionViewDidLoad() {
  }

  loadAuthors() {
    this.authorService.getAuthors().subscribe(authors => {
      this.authors = authors;
    });
  }

  save() {
    this.formValidator.validate(this.formGroup);
    if (!this.formGroup.valid) {
      return;
    }
    let post = <Post>Object.assign(this.post || {}, this.formGroup.value);
    let loading = this.loadingCtrl.create();
    loading.present();
    if (post.id) {
      this.postService.update(post).subscribe(() => {
        loading.dismiss();
        this.dismiss();
      }, () => {
        this.toastSvc.showError("Não foi possível realizar a operação");
        loading.dismiss();
      });
    } else {
      this.postService.create(post).subscribe(() => {
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
