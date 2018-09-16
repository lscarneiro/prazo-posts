import {HttpService} from "./http.service";
import {TokenService} from "./token.service";
import {FormValidatorService} from "./form-validator.service";
import {ConfigService} from "./config.service";
import {ToasterService} from "./toaster.service";

export const CORE_PROVIDERS = [
  HttpService,
  TokenService,
  FormValidatorService,
  ConfigService,
  ToasterService,
];
