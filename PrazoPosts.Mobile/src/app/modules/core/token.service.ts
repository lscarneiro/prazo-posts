import {Injectable} from "@angular/core";

@Injectable()
export class TokenService {

  getAccessToken(): string {
    return localStorage.getItem('access_token');
  }

  setAcessToken(token: string): void {
    localStorage.setItem('access_token', token);
  }
  clear(): void {
    localStorage.clear()
  }
}
