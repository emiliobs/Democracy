using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using Democracy.Models;

namespace Democracy.Controllers
{
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