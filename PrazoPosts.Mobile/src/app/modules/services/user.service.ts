import {Injectable} from "@angular/core";
import {HttpService} from "../core/http.service";
import {ConfigService} from "../core/config.service";
import {BehaviorSubject} from "rxjs";
import {User} from "../../dto/user";

@Injectable()
export class UserService {
  userData: User = null;
  cacheTime = ConfigService.cacheTime;
  fetching: boolean = false;
  tempSubject: BehaviorSubject<User>;

  constructor(private httpService: HttpService) {
    if (!this.tempSubject) {
      this.tempSubject = new BehaviorSubject<User>(null);
    }
    this.tempSubject.subscribe(data => this.userData = data);
  }

  get(): BehaviorSubject<User> {
    if (!this.userData && !this.fetching) {
      this.httpService.get<User>(`users`)
        .subscribe(data => {
          this.fetching = false;
          this.tempSubject.next(data);
          setTimeout(() => {
            this.tempSubject.next(null);
          }, this.cacheTime);
        });
      this.fetching = true;
    }
    return this.tempSubject
  }

  clearCache(): void {
    this.tempSubject.next(null);
  }

}
