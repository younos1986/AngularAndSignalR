# AngularAndSignalR How to use Angular and SignalR in asp.net core 2.2

A part of the sample is taken from <a href='https://code-maze.com/netcore-signalr-angular/'> code-maze.com </a>


  <img
       width="100%" height="500px"
       src="https://raw.githubusercontent.com/younos1986/AngularAndSignalR/master/docs/videos/AngularAndSignalR.gif" type="video/mp4" />
      





# Configuration 

First add SignalR middleware to startup.cs. Then define the hubs to communicate with clients
MapHub is a generic method that accepts a model to transfer
```
      // ConfigureServices
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

      services.AddSignalR();
      
      
      
      // Configure
      app.UseSignalR(routes =>
            {
                routes.MapHub<ChartHub>("/chart");
                routes.MapHub<SignoutHub>("/Signout");

            });
      app.UseMvc();
            
```

Then registers a handler that will be invoked when the hub method with the specified method name (`transferchartSignoutdata` ) is invoked.

the invoke result will be here
```
// Angular SignalRService
this.hubConnection.on('transferchartSignoutdata', (data) => {
      console.log(data);
});

```

Whenever you want to send a message to clients, use `_hub.Clients.All.SendAsync("transferchartSignoutdata", signoutModel);`


# Controller

```
[Route("api/[controller]")]
    [ApiController]
    [Microsoft.AspNetCore.Cors.EnableCors("CorsPolicy")]
    public class SignoutController : ControllerBase
    {
        private IHubContext<SignoutHub> _hub;

        public SignoutController(IHubContext<SignoutHub> hub)
        {
            _hub = hub;
        }
        static SignoutModel signoutModel;
        public IActionResult Get()
        {
            if (signoutModel == null)
                signoutModel = new SignoutModel() { SignOut = false };

            signoutModel.SignOut = !signoutModel.SignOut;

            _hub.Clients.All.SendAsync("transferchartSignoutdata", signoutModel);

            return Ok(new { Message = "Request Completed" });
        }
    }


```

# SignalRService

```


// SignalRService

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



```


# AppComponent

```

import { Component, OnInit } from '@angular/core';
import { SignalRService } from './services/signal-r.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'AngularAndSignalR';

  /** Chart ctor */
  constructor(public signalRService: SignalRService, private http: HttpClient) { }

  ngOnInit() {
    this.signalRService.startConnection('https://localhost:44306/Signout');
    this.signalRService.addTransferChartDataListener();
  }

}



```


