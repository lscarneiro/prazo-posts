import {Author} from "./author";

export interface Post {
  Id: string;
  AuthorId: string;
  Author: Author;
  Title: string;
  Content: string;
}
