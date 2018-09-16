import {Author} from "./author";

export interface Post {
  id: string;
  authorId: string;
  author: Author;
  tittle: string;
  content: string;
}
