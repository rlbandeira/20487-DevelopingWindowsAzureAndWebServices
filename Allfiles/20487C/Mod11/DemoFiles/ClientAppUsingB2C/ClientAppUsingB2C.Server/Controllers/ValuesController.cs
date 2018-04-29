using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ClientAppUsingB2C.Server.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        public string Get()
        {
            return $"Success you finish the demo. You integrated  the WebApi with the client app";
        }
        
    }
}
