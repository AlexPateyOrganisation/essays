import {inject, Injectable} from '@angular/core';
import {Essay} from '../../core/models/essay.model';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {firstValueFrom} from 'rxjs';
import {EssayRequest} from '../../core/contracts/essayRequest.model';

@Injectable({
  providedIn: 'root'
})
export class EssayService {

  private readonly httpClient = inject(HttpClient);

  async getEssay(id: string) : Promise<Essay> {
    const getEssayEndpoint = `${environment.retrieverApiUrl}/essays/${id}`;
    return await firstValueFrom(this.httpClient.get<Essay>(getEssayEndpoint));
  }

  async createEssay(essayRequest: EssayRequest) : Promise<Essay> {
    const createEssayEndpoint = `${environment.writerApiUrl}/essays`;
    return await firstValueFrom(this.httpClient.post<Essay>(createEssayEndpoint, essayRequest));
  }
}
