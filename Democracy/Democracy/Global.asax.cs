using Democracy.Migrations;
using Democracy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Democracy
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Cada que el proyecto corroa el mira si la base de datos obtuvo cambios:(para las migraciones automaticas)
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DemocracyContext,Configuration>());

            this.CheckSuperUser();
        }


        //Método para quemar el super usuario del sistema:
        private void CheckSuperUser()
        {
            var user = new User
            {
                Address = userView.Address,
                FirstName = userView.FirstName,
                Grade = userView.Grade,
                Group = userView.Group,
                LastName = userView.LastName,
                Phone = userView.Phone,
                Photo = picture == string.Empty ? string.Empty : string.Format("~/Content/Photos/{0}", picture),
                userName = userView.userName
            };
        }
    }
}
