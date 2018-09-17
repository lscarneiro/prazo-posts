import {Injectable} from "@angular/core";
import {HttpService} from "../core/http.service";
import {Observable} from "rxjs/Observable";
import {Post} from "../../dto/post";

@Injectable()
export class PostService {

  constructor(private httpService: HttpService) {
  }

  getPosts(): Observable<Post[]> {
    return this.httpService.get<Post[]>(`posts`);
  }

  create(data: Post): Observable<Post> {
    return this.httpService.post<Post>(`posts`, data);
  }

  update(data: Post): Observable<Post> {
    return this.httpService.put<Post>(`posts/${data.Id}`, data);
  }

  delete(id: string): Observable<any> {
    return this.httpService.delete(`posts/${id}`);
  }

}
