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
