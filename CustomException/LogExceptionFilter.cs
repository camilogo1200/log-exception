using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Threading;


namespace CustomException
{
    public class LogExceptionFilterAttribute : ExceptionFilterAttribute, IExceptionFilter

    {
        /// <summary>
        /// Metodo que captura las excepciones
        /// </summary>
        public override Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            Task t = Task.Run(() =>
            {
                GuardadoLog.Instancia.Guardar(actionExecutedContext);
            });
            return t;
        }

    }
}