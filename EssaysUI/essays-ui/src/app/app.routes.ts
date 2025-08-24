import { Routes } from '@angular/router';
import { EssayGet } from './essays/essay-get/essay-get';
import {EssayCreate} from './essays/essay-create/essay-create';

export const routes: Routes = [
  {
    path: 'essays/create',
    component: EssayCreate
  },
  {
    path: 'essays/:id',
    component: EssayGet
  },
];
