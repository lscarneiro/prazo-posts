import {Injectable} from "@angular/core";
import {HttpService} from "../core/http.service";
import {Observable} from "rxjs/Observable";
import {TokenData} from "../../dto/token-data";
import {User} from "../../dto/user";
import {Author} from "../../dto/author";

@Injectable()
export class AuthorService {

  constructor(private httpService: HttpService) {
  }

  getAuthors(): Observable<Author[]> {
    return this.httpService.get<Author[]>(`authors`);
  }
  create(data: Author): Observable<Author> {
    return this.httpService.post<Author>(`authors`, data);
  }
  update(data: Author): Observable<Author> {
    return this.httpService.put<Author>(`authors/${data.Id}`, data);
  }
  delete(id: string): Observable<any> {
    return this.httpService.delete(`authors/${id}`);
  }

}
