using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Democracy.Models;
using System.IO;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace Democracy.Controllers
{
   
    public class UsersController : Controller
    {
        private DemocracyContext db = new DemocracyContext();

        [Authorize(Roles = "User")]
        public ActionResult MySettings()
        {

            //Buscamos el usuarios a editar:
            var user = db.Users.Where(u => u.userName == this.User.Identity.Name).FirstOrDefault();

            //Creo el objeto(UserSettingsView) a partir de otrp objeto(User):
            var view = new UserSettingsView
            {
              Address = user.Address,
              FirstName =user.FirstName,
              Grade = user.Grade,
              Group = user.Group,
              LastName = user.LastName,
              Phone = user.Phone,
              Photo = user.Photo,
              UserId = user.UserId,
              userName = user.userName,
            };


            return View(view);
        }

        [HttpPost]
        public ActionResult MySettings(UserSettingsView view)
        {
            if (ModelState.IsValid)
            {
                string path = string.Empty;
                string picture = string.Empty;

                if (view.Photo != null)
                {
                    picture = Path.GetFileName(view.NewPhoto.FileName);
                    path = Path.Combine(Server.MapPath("~/Content/Photos"), picture);
                    view.NewPhoto.SaveAs(path);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        view.NewPhoto.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                    }
                }

                //find the user in DataBase:
                var user = db.Users.Find(view.UserId);

                user.Address = view.Address;
                user.FirstName = view.FirstName;
                user.Grade = view.Grade;
                user.Group = view.Group;
                user.LastName = view.LastName;
                user.Phone = view.Phone;
               

                if (!string.IsNullOrEmpty(picture))
                {
                    user.Photo = string.Format("~/Content/Photos/{0}", picture);
                }


                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            
            return View (view);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult OnOffAdministrator(int id)
        {
            var user = db.Users.Find(id);

            if (user != null)
            {
                //cuando siepre quiero buscar los usuarios en las tabla ASP.net: de roles y usuarios:
                var userContext = new ApplicationDbContext();
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));

                //Buscamos el role primero:
                var userASP = userManager.FindByEmail(user.userName);

                //Si es diferente de null podemos proceder con el role del ese usuario:
                if (userASP != null)
                {
                    if (userManager.IsInRole(userASP.Id, "Admin"))
                    {
                        userManager.RemoveFromRole(userASP.Id, "Admin");
                    }
                    else
                    {
                        userManager.AddToRole(userASP.Id, "Admin");
                    }
                }

            }     

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        // GET: Users
        public ActionResult Index()
        {
            var userContext = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
            
            var users = db.Users.ToList();
            var usersView = new List<UserIndexView>();

            //por cada usuario que hay en la coleccion users:
            foreach (var user in users)
            {
                var userASP = userManager.FindByEmail(user.userName);

                usersView.Add(new UserIndexView
                {
                    Address = user.Address,
                    Candidates = user.Candidates,
                    FirstName = user.FirstName,
                    Grade = user.Grade,
                    Group = user.Group,
                    GroupMembers = user.GroupMembers,
                    IsAdmin = userASP != null && userManager.IsInRole(userASP.Id, "Admin"),
                    LastName = user.LastName,
                    Phone = user.Phone,
                    Photo = user.Photo,
                    UserId = user.UserId,
                    userName = user.userName,


                });
            }

            return View(usersView);
        }

        [Authorize(Roles = "Admin")]
        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [Authorize(Roles = "Admin")]
        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserView userView)
        {
            if (!ModelState.IsValid)
            {
                return View(userView);  
            }

            //Upload Image:
            string path = string.Empty;
            string picture = string.Empty;

            if (userView.Photo != null)
            {
                picture = Path.GetFileName(userView.Photo.FileName);
                path = Path.Combine(Server.MapPath("~/Content/Photos"),picture);
                userView.Photo.SaveAs(path);

                using (MemoryStream ms = new MemoryStream())
                {
                    userView.Photo.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }
            }

            //Save record:
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
            
            db.Users.Add(user);

            try
            {
                db.SaveChanges();
                this.createASPUser(userView);

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && 
                    ex.InnerException.InnerException != null 
                    && ex.InnerException.InnerException.Message.Contains("userNameIndex"))
                {
                    ViewBag.Error = "The Email has already used for another User.";
                }
                else
                {
                    ViewBag.Error = ex.Message;
                }

                return View(userView);
            }

            return RedirectToAction("Index");

           
        }

        private void createASPUser(UserView userView)
        {
            //User management:
            var userContext = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(userContext));

            //Create User Role:
            string roleName = "User";

            //Check to see if Role Exist, if not create it:
            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new IdentityRole(roleName));

            }

            //Create the ASP NET User:
            var userASP = new ApplicationUser
            {
                UserName = userView.userName,
                Email = userView.userName,
                PhoneNumber = userView.Phone,
            };

            userManager.Create(userASP, userASP.UserName);

            //Add user to role:
            userASP = userManager.FindByName(userView.userName);
            userManager.AddToRole(userASP.Id, "User");
        }

        [Authorize(Roles = "Admin")]
        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var userView = new UserView
            {
                UserId = user.UserId,
                Address = user.Address,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Grade = user.Grade,
                Group = user.Group,
                Phone = user.Phone,
                userName = user.userName,
            };

            return View(userView);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserView userView)
        {
            if (!ModelState.IsValid)
            {

                return View(userView);
                
            }

            //Upload Image:
            string path = string.Empty;
            string picture = string.Empty;

            if (userView.Photo != null)
            {
                picture = Path.GetFileName(userView.Photo.FileName);
                path = Path.Combine(Server.MapPath("~/Content/Photos"), picture);
                userView.Photo.SaveAs(path);

                using (MemoryStream ms = new MemoryStream())
                {
                    userView.Photo.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }
            }

            var user = db.Users.Find(userView.UserId);

            user.Address = userView.Address;
            user.FirstName = userView.FirstName;
            user.Grade = userView.Grade;
            user.Group = userView.Group;
            user.LastName = userView.LastName;
            user.Phone = userView.Phone;

            if (!string.IsNullOrEmpty(picture))
            {
                user.Photo = string.Format("~/Content/Photos/{0}", picture);
            }


            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        [Authorize(Roles = "Admin")]
        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
          
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                if (ex.InnerException != null && ex.InnerException.InnerException != null && 
                    ex.InnerException.InnerException.Message.Contains("REFERENCE"))
                {
                    //Capturamos este error, ya que es causado por la violación de una relacón primary key:(llava promaria)
                    ModelState.AddModelError(string.Empty,"The record can't be delete, because has related Records");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, ex.Message); 
                }

                return View(user);
            }
          
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
