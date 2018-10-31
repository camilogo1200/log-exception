using CustomException;
using System;
using System.Web.Http;

namespace Program.Controllers
{
    public class PruebaController : ApiController
    {
        [LogExceptionFilter]
        public IHttpActionResult Get( string id )
        {
            throw new Exception("Mensaje Exception :D");
        }

        public IHttpActionResult PostNombreMetodo( PruebaWrapper id )
        {
            return Ok(id);
        }

        // int uh = 14;
        //throw new Exception("sukajdgjkd");
        // int pru2 = Convert.ToInt32(id);
    }
}