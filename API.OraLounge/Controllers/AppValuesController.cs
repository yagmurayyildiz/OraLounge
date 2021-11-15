using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;

namespace API.OraLounge.Controllers
{
    public class AppValuesController : ApiController
    {
        [HttpGet]
        [ResponseType(typeof(string))]
        public HttpResponseMessage Get()
        {
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent("Minimum spend per person £10\nMaximum staying time is 2 hours", Encoding.UTF8, "text/plain");
            return res;
        }
    }
}
