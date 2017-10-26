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
            string df = null;

            //pruebaWrapper prueba = new pruebaWrapper();

            //prueba.pru2 = df.ToUpper();
            bool aplicaConvenio = false;

            WPF.Comun.ServicioAdmisionMensajeria.ADPeaton pp = new WPF.Comun.ServicioAdmisionMensajeria.ADPeaton
            {
                Apellido1 = df,

            };

            PropertiesValidator<WPF.Comun.ServicioAdmisionMensajeria.ADPeaton> validador = new PropertiesValidator<WPF.Comun.ServicioAdmisionMensajeria.ADPeaton>();
            validador.Validate(pp);

            //WPF.Comun.ServicioAdmisionMensajeria.ADMensajeriaTipoCliente destinatarioRemitente = new WPF.Comun.ServicioAdmisionMensajeria.ADMensajeriaTipoCliente
            //{
            //    FacturaRemitente = false,
            //    PeatonDestinatario = !aplicaConvenio ? new WPF.Comun.ServicioAdmisionMensajeria.ADPeaton
            //    {
            //        Apellido1 = df.Trim(),

            //    } : null

            //};
            }




        // int uh = 14;
        //throw new Exception("sukajdgjkd");
        // int pru2 = Convert.ToInt32(id);


    }
}

