import { Routes } from '@angular/router';
import { EssayGet } from './essays/essay-get/essay-get';

export const routes: Routes = [
  {
    path: 'essays/:id',
    component: EssayGet
  },
];
