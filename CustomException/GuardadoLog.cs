using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Web.Configuration;
using System.Web.Http.Filters;
using WPF.Comun.ServicioAdmisionMensajeria;

namespace CustomException
{
    public class GuardadoLog
    {
        #region Instancia

        /// <summary>
        /// Atributo utilizado para evitar problemas con multithreading en el singleton.
        /// </summary>
        private static readonly object syncRoot = new Object();

        private const string SeparadorException = "###################################################";
        private const string Separador = "---------------------------------------------------";
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

        #endregion Instancia

        /// <summary>
        /// Metodo de verificar y/o crear las carpetas y archivos para el log de excepciones,
        /// guarda el conteniodo de las excepciones.
        /// </summary>
        /// <param name="exep"></param>
        public void Guardar( HttpActionExecutedContext exep )
        {
            string Nombre = WebConfigurationManager.AppSettings["Nombre"];
            string Ruta = @"" + WebConfigurationManager.AppSettings["Ruta"];
            string Archivo = @"" + Ruta + Nombre + ".log";
            if (!Directory.Exists(Ruta))
            {
                Directory.CreateDirectory(Ruta);
            }

            Exception ex = exep.Exception;
            StackTrace st = new StackTrace(ex.GetBaseException(), true);
            string mensaje = st.ToString();
            StackFrame frame = st.GetFrame(0);
            MemberTypes methodClass = frame.GetMethod().MemberType;
            String className = frame.GetMethod().DeclaringType.Name;
            String method = frame.GetMethod().Name;
            int line = frame.GetFileLineNumber();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(SeparadorException);
            sb.Append("# ServerTime : ").AppendLine(DateTime.Now.ToString());
            sb.Append("# Origin : ").AppendLine(frame.GetMethod().ReflectedType.UnderlyingSystemType.ToString());
            sb.AppendLine(Separador);
            sb.Append("# NameSpace : ").AppendLine(ex.Source);
            sb.Append("# Class : ").AppendLine(className);
            sb.Append("# ").Append(methodClass.ToString()).Append(" : ").AppendLine(method);
            sb.Append("# Line : ").AppendLine(line.ToString());
            sb.Append("# Excepcion Message : ").AppendLine(ex.Message);
            if (ex.InnerException != null)
            {
                sb.AppendLine(Separador);
                sb.Append("# InnerException : ").AppendLine(ex.InnerException.ToString());
                sb.Append("# Inner Exception Message : ").AppendLine(ex.InnerException.Message);
            }
            sb.AppendLine(Separador);
            sb.AppendLine("# StackTrace");
            sb.AppendLine(Separador);
            sb.AppendLine(mensaje);
            if (ex is FaultException<ControllerException> faultException)
            {
                ControllerException exc = ( faultException ).Detail;
                sb.AppendLine(Separador);
                sb.AppendLine("# Tipo Error Controller: ").AppendLine(exc.TipoError);
                sb.AppendLine("# Mensaje Controller: ").AppendLine(exc.Mensaje);
            }
            sb.AppendLine(SeparadorException);
            using (StreamWriter writer = new StreamWriter(Archivo, true))
            {
                writer.WriteLine(sb.ToString());
            }
            AddEventViewerEntry(Nombre, sb.ToString());
        }

        private void AddEventViewerEntry( string applicationName, string v )
        {
            //if (!EventLog.SourceExists(applicationName))
            //{
            //    EventLog.CreateEventSource(applicationName, applicationName);
            //}

            //using (EventLog eventLogApplication = new EventLog(applicationName))
            //{
            //    eventLogApplication.Source = applicationName;
            //    eventLogApplication.WriteEntry(v, EventLogEntryType.Information, 101, 1);
            //}

            using (EventLog eventLogGeneral = new EventLog("Application"))
            {
                eventLogGeneral.Source = "Application";
                eventLogGeneral.WriteEntry(v, EventLogEntryType.Error, 101, 1);
            }
        }
    }
}