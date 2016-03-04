using Democracy.Migrations;
using Democracy.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
            //me conecto all db:
            var userContext = new ApplicationDbContext();
            //Aqui controlo los usuarios:
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
            var db = new DemocracyContext();
            this.CheckRole("Admin", userContext);
            this.CheckRole("User", userContext);

            //validar si el usuario existe en la tabla user:
            var user = db.Users.Where(u=>u.userName.ToLower().Equals("barrera_emilio@hotmail.com")).FirstOrDefault();

            if (user == null)
            {
                //creamos nuestro usuario Administrador:
                user = new User
                {
                    Address = "Calle Madrid 55",
                    FirstName = "Emilio",
                    LastName = "Barrera",
                    Phone = "693661995",
                    userName = "barrera_emilio@hotmail.com",
                    Photo = "~/Content/Photos/fondo.jpg"
                };

                db.Users.Add(user);
                db.SaveChanges();
            }

            //valido si el uusarip existe en las tablas de the ASP NET User:
            var userASP = userManager.FindByName(user.userName);

            if (userASP == null)
            {
                userASP = new ApplicationUser
                {
                  UserName = user.userName,
                  Email = user.userName,
                  PhoneNumber = user.Phone                
                 
                };

                userManager.Create(userASP, "Eabs+++++55555");
                userManager.AddToRole(userASP.Id, "Admin");
            }

            userManager.AddToRole(userASP.Id, "Admin");
        }

        private void CheckRole(string roleName, ApplicationDbContext userContext)
        {
            
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(userContext));

            //Check to see Role Exists, if not create it.
            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new IdentityRole(roleName));
            }
        }
    }
}
