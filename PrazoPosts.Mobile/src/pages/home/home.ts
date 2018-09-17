import {Component} from '@angular/core';
import {AlertController, Loading, LoadingController, ModalController, NavController} from 'ionic-angular';
import {Post} from "../../app/dto/post";
import {PostService} from "../../app/modules/services/post.service";
import {EditPost} from "./edit-post/edit-post";
import {Author} from "../../app/dto/author";

@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})
export class HomePage {

  posts: Post[] = [];
  constructor(public navCtrl: NavController,
              private loadingCtrl: LoadingController,
              private alertCtrl: AlertController,
              private postService: PostService,
              public modalCtrl: ModalController) {
    this.loadData();

  }
  ionViewDidLoad() {

  }

  loadData(loading: Loading = null) {
    if (!loading) {
      loading = this.loadingCtrl.create();
      loading.present();
    }
    this.postService.getPosts().subscribe(posts => {
      loading.dismiss();
      this.posts = posts;
    }, () => {
      loading.dismiss();
    })
  }

  editPost(post: Post = null) {
    let authorModal = this.modalCtrl.create(EditPost, {post: post});
    authorModal.present();
    authorModal.onDidDismiss(() => {
      this.loadData();
    })
  }


  private executeDelete(post: Post) {
    let loading = this.loadingCtrl.create();
    loading.present();
    this.postService.delete(post.Id).subscribe(() => {
      this.loadData(loading);
    }, () => {
      loading.dismiss();
    });
  }

  delete(post: Post) {
    let alert = this.alertCtrl.create({
      title: 'Ação irreversível',
      message: 'Os dados do posts serão permanentemente excluídos, deseja prosseguir?',
      buttons: [
        {
          text: 'Manter',
          handler: () => {
          }
        },
        {
          text: 'Excluir',
          handler: () => {
            this.executeDelete(post);
          }
        }
      ]
    });

    alert.present();
  }
}
