import {Injectable} from "@angular/core";
import {HttpClient, HttpErrorResponse, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs/Observable";
import {catchError, timeout} from "rxjs/operators";
import {of} from "rxjs/observable/of";
import {ConfigService} from "./config.service";
import {TimeoutError} from "rxjs/util/TimeoutError";
import {ToasterService} from "./toaster.service";
import {_throw} from 'rxjs/observable/throw';
import {TokenService} from "./token.service";
import {Subject} from "rxjs";
import {ServerValidationMap} from "./model/server-validation-map";

@Injectable()
export class HttpService {
  private timeoutConfig = ConfigService.timeoutConfig;

  constructor(private httpClient: HttpClient,
              private toastSvc: ToasterService,
              private tokenService: TokenService) {
  }

  onServerValidationErrors = new Subject<ServerValidationMap>();

  get<T>(url): Observable<T> {
    return this.httpClient.get<T>(`${ConfigService.API_URL}/${url}`, {
      headers: this.defaultHeaders()
    }).pipe(
      timeout(this.timeoutConfig),
      catchError(err => {
        return this.handleError(err, url);
      })
    );
  }

  post<T>(url, data): Observable<T> {
    return this.httpClient.post<T>(`${ConfigService.API_URL}/${url}`, data, {
      headers: this.defaultHeaders()
    }).pipe(
      timeout(this.timeoutConfig),
      catchError(err => {
        return this.handleError(err, url);
      })
    );
  }

  delete<T>(url): Observable<T> {
    return this.httpClient.delete<T>(`${ConfigService.API_URL}/${url}`, {
      headers: this.defaultHeaders()
    }).pipe(
      timeout(this.timeoutConfig),
      catchError(err => {
        return this.handleError(err, url);
      })
    );
  }

  put<T>(url, data): Observable<T> {
    return this.httpClient.put<T>(`${ConfigService.API_URL}/${url}`, data, {
      headers: this.defaultHeaders()
    }).pipe(
      timeout(this.timeoutConfig),
      catchError(err => {
        return this.handleError(err, url);
      })
    );
  }

  patch<T>(url, data): Observable<T> {
    return this.httpClient.patch<T>(`${ConfigService.API_URL}/${url}`, data, {
      headers: this.defaultHeaders()
    }).pipe(
      timeout(this.timeoutConfig),
      catchError(err => {
        return this.handleError(err, url);
      })
    );
  }

  private defaultHeaders(): HttpHeaders {
    let token = this.tokenService.getAccessToken();
    if (token) {
      return new HttpHeaders().set(
        'Authorization', `Bearer ${token}`
      );
    } else {
      return null;
    }
  }

  private handleError(err, url): Observable<any> {
    if (err instanceof TimeoutError) {
      this.toastSvc.showError('Cheque sua rede e tente novamente.')
      return of(null);
    } else if (err instanceof HttpErrorResponse) {
      if (err.status == 401) {
        this.toastSvc.showError('Você não tem acesso à essa informação')
        return of(null);
      } else if (err.status == 400 && err.error.validationErrors) {
        this.onServerValidationErrors.next(err.error.validationErrors);
      } else if ((err.status == 400 || err.status == 500 || err.status == 404) && err.error.error) {
        this.toastSvc.showError(err.error.error);
        return of(null);
      } else {
        return _throw(err);
      }
    } else {
      return _throw(err);
    }
  }
}
