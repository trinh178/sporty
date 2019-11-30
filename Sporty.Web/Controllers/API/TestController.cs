using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using System.Web;

namespace Sporty.Web.API.Controllers
{
    public class TestController : ApiController
    {
        public string Get()
        {
            //var file = File.Create(Directory.GetCurrentDirectory());
            //file.SaveAs(Path.Combine(directory, fileName));
            //var file = new P
            
            return HttpContext.Current.Server.MapPath("~/uploads/logo");
        }
    }
}