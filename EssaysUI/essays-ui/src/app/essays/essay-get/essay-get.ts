import {Component, inject, input, OnInit} from '@angular/core';
import {EssayService} from '../services/essay.service';
import {Essay} from '../../core/models/essay.model';
import {NzCardComponent} from 'ng-zorro-antd/card';
import {DatePipe} from '@angular/common';
import {NzSkeletonComponent} from 'ng-zorro-antd/skeleton';

@Component({
  selector: 'app-essay-get',
  imports: [
    NzCardComponent,
    DatePipe,
    NzSkeletonComponent
  ],
  templateUrl: './essay-get.html',
  styleUrl: './essay-get.scss'
})
export class EssayGet implements OnInit {

  private readonly essayService = inject(EssayService);
  public readonly id = input.required<string>();
  public essay: Essay | null = null;

  async ngOnInit(): Promise<void> {
    this.essay = await this.essayService.getEssay(this.id());
  }
}
