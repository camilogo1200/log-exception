using CustomException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Program.Controllers
{
    public class PruebaController : ApiController
    {
        [LogExceptionFilter]
        public IHttpActionResult get(string id)
        {
            doSomething(id);
            return Ok("jdjk");
        }
        [LogExceptionFilter]
        private void doSomething(string id)
        {
            // int uh = 14;
            //throw new Exception("sukajdgjkd");
           int pru2 = Convert.ToInt32(id);
            throw new NotImplementedException();

        }
    }
}
