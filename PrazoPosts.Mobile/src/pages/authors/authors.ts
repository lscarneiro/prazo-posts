import {Component} from '@angular/core';
import {AlertController, Loading, LoadingController, ModalController, NavController} from 'ionic-angular';
import {Author} from "../../app/dto/author";
import {AuthorService} from "../../app/modules/services/author.service";
import {EditAuthor} from "./edit-author/edit-author";

@Component({
  selector: 'page-authors',
  templateUrl: 'authors.html'
})
export class AuthorsPage {

  authors: Author[] = [];

  constructor(public navCtrl: NavController,
              private alertCtrl: AlertController,
              private authorService: AuthorService,
              private loadingCtrl: LoadingController,
              public modalCtrl: ModalController) {

  }

  ionViewDidLoad() {
    this.loadData();
  }

  loadData(loading: Loading = null) {
    if (!loading) {
      loading = this.loadingCtrl.create();
      loading.present();
    }
    this.authorService.getAuthors().subscribe(authors => {
      loading.dismiss();
      this.authors = authors;
    }, () => {
      loading.dismiss();
    })
  }

  editAuthor(author: Author = null) {
    let authorModal = this.modalCtrl.create(EditAuthor, {author: author});
    authorModal.present();
    authorModal.onDidDismiss(() => {
      this.loadData();
    })
  }

  private executeDelete(author: Author) {
    let loading = this.loadingCtrl.create();
    loading.present();
    this.authorService.delete(author.id).subscribe(() => {
      this.loadData(loading);
    }, () => {
      loading.dismiss();
    });
  }

  delete(author: Author) {
    let alert = this.alertCtrl.create({
      title: 'Ação irreversível',
      message: 'Excluindo o autor, todos os posts serão removidos, deseja prosseguir?',
      buttons: [
        {
          text: 'Manter',
          handler: () => {
          }
        },
        {
          text: 'Excluir',
          handler: () => {
            this.executeDelete(author);
          }
        }
      ]
    });

    alert.present();
  }
}
