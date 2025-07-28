import {Component, inject, input, OnInit, signal} from '@angular/core';
import {EssayService} from '../services/essay.service';
import {Essay} from '../../core/models/essay.model';

@Component({
  selector: 'app-essay-get',
  imports: [],
  templateUrl: './essay-get.html',
  styleUrl: './essay-get.scss'
})
export class EssayGet implements OnInit {

  private readonly essayService = inject(EssayService);
  public readonly id = input.required<string>();
  public essay = signal<Essay | null>(null);

  ngOnInit(): void {
    let essay = this.essayService.getEssay(this.id());

    if (essay == null) {

    }

    this.essay.set(essay);
  }
}


