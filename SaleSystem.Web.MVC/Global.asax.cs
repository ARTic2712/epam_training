using SaleSystem.Web.MVC.Models;
using SaleSystem.Web.MVC.SupportClasses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaleSystem.Web.MVC.Models;
using SaleSystem.Web.MVC.Interfaces;
using SaleSystem.Web.MVC.Repositories;



using System.Web.Optimization;
using System.Web.Routing;

namespace SaleSystem.Web.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
