import {CORE_PROVIDERS} from "./core";
import {SERVICES_PROVIDERS} from "./services";

export const MODULES_PROVIDERS = [
  ...CORE_PROVIDERS,
  ...SERVICES_PROVIDERS,
];
