import {inject, Injectable, signal} from '@angular/core';
import {Essay} from '../../core/models/essay.model';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EssayService {

  private readonly httpClient = inject(HttpClient);

  getEssay(id: string) : Essay | null {

    let getEssayEndpoint = environment.retrieverApiUrl + '/essays/' + id;

    this.httpClient.get(getEssayEndpoint).subscribe({
      next: (resData) => {
        console.log(resData);
      }
    });

    return null;
  }
}
