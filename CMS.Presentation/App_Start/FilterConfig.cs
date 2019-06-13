﻿using CMS.Presentation.Filters;
using System.Web;
using System.Web.Mvc;

namespace CMS.Presentation
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new WeborisationFilter());

        }
    }
}
