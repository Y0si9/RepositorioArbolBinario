﻿using System.Web;
using System.Web.Mvc;

namespace AplicacionArbol9B
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
