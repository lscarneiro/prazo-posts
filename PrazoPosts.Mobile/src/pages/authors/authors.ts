import {Component} from '@angular/core';
import {NavController} from 'ionic-angular';
import {Author} from "../../app/dto/author";
import {AuthorService} from "../../app/modules/services/author.service";

@Component({
  selector: 'page-authors',
  templateUrl: 'authors.html'
})
export class AuthorsPage {

  authors: Author[] = [];

  constructor(public navCtrl: NavController,
              private authorService: AuthorService) {

  }

  ionViewDidLoad() {
    this.authorService.getAuthors().subscribe(authors => {
      this.authors = authors;
    })
  }

  addAuthor() {
    
  }
}
