import {AuthService} from "./auth.service";
import {UserService} from "./user.service";
import {PostService} from "./post.service";
import {AuthorService} from "./author.service";

export const SERVICES_PROVIDERS = [
  AuthService,
  UserService,
  PostService,
  AuthorService
];
