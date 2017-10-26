using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Web.Http.Filters;
using System.Web.Configuration;
using System.Runtime.Serialization;
using System.Security;
using System.ServiceModel.Channels;
using WPF.Comun.ServicioAdmisionMensajeria;
using System.ServiceModel;

namespace CustomException
{
    public class GuardadoLog
    {
        #region Instancia
        /// <summary>
        /// Atributo utilizado para evitar problemas con multithreading en el singleton.
        /// </summary>
        private static object syncRoot = new Object();

        private static volatile GuardadoLog instancia;

        public static GuardadoLog Instancia
        {
            get
            {
                if (instancia == null)
                {
                    lock (syncRoot)
                    {
                        if (instancia == null)
                        {
                            instancia = new GuardadoLog();
                        }
                    }
                }
                return instancia;
            }
        }
        #endregion
        /// <summary>
        /// Metodo de verificar y/o crear las carpetas y archivos para el log de excepciones,
        /// guarda el conteniodo de las excepciones.
        /// </summary>
        /// <param name="exep"></param>
        public void Guardar(HttpActionExecutedContext exep)
        {
            string Nombre = WebConfigurationManager.AppSettings["Nombre"].ToString();
            string Ruta = @"" + WebConfigurationManager.AppSettings["Ruta"].ToString(); ;
            string Archivo = @"" + Ruta + Nombre + ".log";
            if (!Directory.Exists(Ruta))
            {
                Directory.CreateDirectory(Ruta);
            }


            Exception ex = exep.Exception;
            string Separador = "=================================================== \r\n";
            String msg = Separador + DateTime.Now.ToString() + "\r\n" + Separador + " NameSpace: {0}, \r\n Class: {1},\r\n {2}: {3}, \r\n Line: {4}, \r\n Excepcion: {5} \r\n ";

            if (ex is FaultException<ControllerException>)
            {
                ControllerException exc = ((FaultException<ControllerException>)ex).Detail;
                msg += "\r\n Tipo Error Controller: " + exc.TipoError + "\r\n Mensaje Controller: " + exc.Mensaje;
            }
            StackTrace st = new StackTrace(ex.GetBaseException(), true);
            string mensaje = st.ToString();
            StackFrame frame = st.GetFrame(0);
            MemberTypes methodClass = frame.GetMethod().MemberType;
            String clss = frame.GetMethod().DeclaringType.Name;
            String method = frame.GetMethod().Name;
            int line = frame.GetFileLineNumber();
            string Excep = String.Format(msg, ex.Source, clss, methodClass, method, line, ex.Message + " - " + mensaje);
            using (StreamWriter writer = new StreamWriter(Archivo, true))
            {
                writer.WriteLine(Excep);
            }
        }
    }
}
