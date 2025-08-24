import {AuthorRequest} from './authorRequest.model';

export interface EssayRequest {
  title: string,
  body: string,
  authors: AuthorRequest[]
}
