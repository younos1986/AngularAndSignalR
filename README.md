# AngularAndSignalR: How to use Angular and SignalR in asp.net core 2.2 (Learning Purpose)


A part of the sample is taken from <a href='https://code-maze.com/netcore-signalr-angular/'> code-maze.com </a>


  <img
       width="100%" height="500px"
       src="https://raw.githubusercontent.com/younos1986/AngularAndSignalR/master/docs/videos/AngularAndSignalR.gif" type="video/mp4" />
      


# What is <a href='https://docs.microsoft.com/en-us/aspnet/core/signalr/introduction?view=aspnetcore-2.2'> SignalR </a>?

ASP.NET Core SignalR is an open-source library that simplifies adding real-time web functionality to apps. Real-time web functionality enables server-side code to push content to clients instantly.


# Hubs

SignalR uses hubs to communicate between clients and servers.

A hub is a high-level pipeline that allows a client and server to call methods on each other. SignalR handles the dispatching across machine boundaries automatically, allowing clients to call methods on the server and vice versa. You can pass strongly-typed parameters to methods, which enables model binding. SignalR provides two built-in hub protocols: a text protocol based on JSON and a binary protocol based on MessagePack. MessagePack generally creates smaller messages compared to JSON. Older browsers must support XHR level 2 to provide MessagePack protocol support.

Hubs call client-side code by sending messages that contain the name and parameters of the client-side method. Objects sent as method parameters are deserialized using the configured protocol. The client tries to match the name to a method in the client-side code. When the client finds a match, it calls the method and passes to it the deserialized parameter data.


# Installation

```

npm install @aspnet/signalr –-save

npm install ng2-charts --save

npm install chart.js --save

// in Angular.json 

"scripts": [
              "./node_modules/chart.js/dist/Chart.js"
            ],

```

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

  /** AppComponent ctor */
  constructor(public signalRService: SignalRService, private http: HttpClient) { }

  ngOnInit() {
    this.signalRService.startConnection('https://localhost:44306/Signout');
    this.signalRService.addTransferChartDataListener();
  }

}



```




# Users and Groups

To manage connections 

```

using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace AngularAndSignalR.SignalRConfiguration.Signouts
{
    public class SignoutHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            // Context.ConnectionId
            // Context.User

			//Adding and removing users
            //Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            //Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
			
            //var name = Context.User.Identity.Name;
            //using (var db = new UserContext())
            //{
            //    var user = db.Users
            //        .Include(u => u.Connections)
            //        .SingleOrDefault(u => u.UserName == name);

            //    if (user == null)
            //    {
            //        user = new User
            //        {
            //            UserName = name,
            //            Connections = new List<Connection>()
            //        };
            //        db.Users.Add(user);
            //    }

            //    user.Connections.Add(new Connection
            //    {
            //        ConnectionID = Context.ConnectionId,
            //        UserAgent = Context.Request.Headers["User-Agent"],
            //        Connected = true
            //    });
            //    db.SaveChanges();
            //}

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            // Context.ConnectionId
            // Context.User

            //using (var db = new UserContext())
            //{
            //    var connection = db.Connections.Find(Context.ConnectionId);
            //    connection.Connected = false;
            //    db.SaveChanges();
            //}

            return base.OnDisconnectedAsync(exception);
        }
    }
}



```


# Adding and removing users

```  
        Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
			
```



