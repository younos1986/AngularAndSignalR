import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ChartComponent } from './chart/chart.component';
import { ChatComponent } from './chat/chat.component';

const routes: Routes = [

  {
    path: 'chart',
    component: ChartComponent
  },
  {
    path: 'chat',
    component: ChatComponent
  },

];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
