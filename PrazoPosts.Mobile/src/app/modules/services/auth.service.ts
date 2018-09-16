import {Injectable} from "@angular/core";
import {HttpService} from "../core/http.service";
import {Observable} from "rxjs/Observable";
import {TokenData} from "../../dto/token-data";
import {User} from "../../dto/user";

@Injectable()
export class AuthService {

  constructor(private httpService: HttpService) {
  }

  authenticate(data: User): Observable<TokenData> {
    return this.httpService.post<TokenData>(`auth`, data);
  }

}
