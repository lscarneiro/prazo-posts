import {Author} from "./author";

export interface Post {
  id: string;
  authorId: string;
  author: Author;
  title: string;
  content: string;
}
