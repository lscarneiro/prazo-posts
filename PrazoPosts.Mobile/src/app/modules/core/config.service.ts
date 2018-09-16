import {Injectable} from "@angular/core";

@Injectable()
export class ConfigService {
  public static API_URL = 'http://localhost:5000';

  public static timeoutConfig: number = 30000;
  public static cacheTime: number = 10000;

}
