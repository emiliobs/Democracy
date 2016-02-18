using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using Democracy.Models;
using System.Net;
using System.Data.Entity;

namespace Democracy.Controllers
{
    [Authorize]
    public class StatesController : Controller
    {
        private DemocracyContext db = new DemocracyContext();
        // GET: States
        [HttpGet]
        public ActionResult Index()
        {
            return View(db.States.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(State state)
        {
            //Valido si el modelo es valido:
            if (!ModelState.IsValid)
            {
                return View(state);
            }

            db.States.Add(state);
            db.SaveChanges();

            //Lo redirecciono a la vista Index(o a la que to desee)
            return RedirectToAction("Index");
        }

         [HttpGet]
         public ActionResult Edit(int? id)
         {
            //si no existe id, me samuestrra un error controlado:
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            var state = db.States.Find(id);

            //si no existe id, me samuestrra un error controlado:
            if (state == null)
            {
                return HttpNotFound();
            }


            //Me retorna a la plantilla edit
            return View(state);
         }

        [HttpPost]
        public ActionResult Edit(State state)
        {
            if (!ModelState.IsValid)
            {
                return View(state);
                   
            }      
            db.Entry(state).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var state = db.States.Find(id);

            if (state == null)
            {
                return HttpNotFound();

            }

            return View(state);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var state = db.States.Find(id);

            if (state == null)
            {
                return HttpNotFound();
            }

            return View(state);
        }

        [HttpPost]
        public ActionResult Delete(int id, State state)
        {
            state = db.States.Find(id);
            
            if (state == null)
            {
                return HttpNotFound();
            }

            db.States.Remove(state);

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                //Error reference cuasado por claves primarias:
                if (ex.InnerException != null && 
                    ex.InnerException.InnerException != null && 
                    ex.InnerException.InnerException.Message.Contains("REFERENCE"))
                {
                    ViewBag.Error = "Can't delete the record, because has related records.....";
                    return View(state);
                }
                else
                {
                    ViewBag.Error = ex.Message;
                    return View(state);
                }

                //return View(state);
            }

           
            return RedirectToAction("Index");         
        }

        //Desconectar la BD:
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