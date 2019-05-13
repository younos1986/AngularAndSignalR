using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularAndSignalR.SignalRConfiguration;
using AngularAndSignalR.SignalRConfiguration.Signouts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AngularAndSignalR.Controllers
{
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
}
