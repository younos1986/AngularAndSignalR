import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';


import { ChartsModule } from 'ng2-charts';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { ChartComponent } from './chart/chart.component';
import { ChatComponent } from './chat/chat.component';
import { SignalRService } from './services/signal-r.service';


@NgModule({
  declarations: [
    AppComponent,
    ChartComponent,
    ChatComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ChartsModule,
    HttpClientModule
  ],
  providers: [
    SignalRService 
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
