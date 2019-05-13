import { Injectable } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { ChartModel } from '../_interfaces/chartmodel.model';

@Injectable()
export class SignalRService {
  public data: ChartModel[];

  private hubConnection: signalR.HubConnection

  public startConnection = (url: string) => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(url)
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))
  }

  public addTransferChartDataListener = () => {

    this.hubConnection.on('transferchartdata', (data) => {

      this.data = data;
      console.log(data);

    });

    this.hubConnection.on('transferchartSignoutdata', (data) => {
      console.log(data);
    });


  }
}
