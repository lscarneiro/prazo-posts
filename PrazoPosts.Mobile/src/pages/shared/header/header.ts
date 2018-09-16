import {Component, Input} from "@angular/core";
import {App} from "ionic-angular";
import {UserService} from "../../../app/modules/services/user.service";


@Component({
  selector: '[mainHeader]',
  templateUrl: 'header.html',
})
export class HeaderComponent {

  constructor(private app: App,
              private userService: UserService
  ) {
    this.init();
  }

  @Input() title: string = 'LawBlog';


  init() {
  }

}
