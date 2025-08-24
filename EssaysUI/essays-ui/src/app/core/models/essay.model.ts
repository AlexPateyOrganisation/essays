import {Author} from './author.model';

export interface Essay {
  id: string,
  title: string,
  body: string,
  authors: Author[],
  createdWhen: Date
}
