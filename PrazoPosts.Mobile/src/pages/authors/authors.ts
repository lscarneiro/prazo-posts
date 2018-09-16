import {Component} from '@angular/core';
import {ModalController, NavController} from 'ionic-angular';
import {Author} from "../../app/dto/author";
import {AuthorService} from "../../app/modules/services/author.service";
import {AddAuthor} from "./add-author/add-author";

@Component({
  selector: 'page-authors',
  templateUrl: 'authors.html'
})
export class AuthorsPage {

  authors: Author[] = [];

  constructor(public navCtrl: NavController,
              private authorService: AuthorService,
              public modalCtrl: ModalController) {

  }

  ionViewDidLoad() {
    this.authorService.getAuthors().subscribe(authors => {
      this.authors = authors;
    })
  }

  addAuthor() {
    let myModal = this.modalCtrl.create(AddAuthor);
    myModal.present();
  }
}
