using CustomException;
using System.Web;
using System.Web.Mvc;
using System;

namespace Program
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
           filters.Add(new HandleErrorAttribute());
       }

  
    }
}
