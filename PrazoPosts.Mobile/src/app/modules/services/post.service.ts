import {Injectable} from "@angular/core";
import {HttpService} from "../core/http.service";
import {Observable} from "rxjs/Observable";
import {Post} from "../../dto/post";

@Injectable()
export class PostService {

  constructor(private httpService: HttpService) {
  }

  getPosts(): Observable<Post> {
    return this.httpService.get<Post>(`posts`);
  }

}
