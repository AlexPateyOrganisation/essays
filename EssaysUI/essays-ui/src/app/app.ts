import { Component } from '@angular/core';
import {RouterLink, RouterOutlet} from '@angular/router';
import {NzPageHeaderComponent} from 'ng-zorro-antd/page-header';
import {NzLayoutComponent, NzSiderComponent} from 'ng-zorro-antd/layout';
import {NzMenuModule} from 'ng-zorro-antd/menu';
import {NzIconDirective} from 'ng-zorro-antd/icon';
import {NzFlexDirective} from 'ng-zorro-antd/flex';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NzPageHeaderComponent, NzLayoutComponent, NzSiderComponent, NzMenuModule, NzIconDirective, NzFlexDirective, RouterLink],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App { }
