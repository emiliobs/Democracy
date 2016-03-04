using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Democracy.Models;

namespace Democracy.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GroupsController : Controller
    {
        private DemocracyContext db = new DemocracyContext();

        //Métodos Vista AddMember:

        [HttpGet]
        public ActionResult DeleteMember(int id)
        {
            var member = db.GroupMembers.Find(id);

            if (member != null)
            {
                db.GroupMembers.Remove(member);
                db.SaveChanges();
            }

            return RedirectToAction(string.Format("Details/{0}", member.GroupId));
        }

        [HttpGet]
        public ActionResult AddMember(int groupId)
        {

            ViewBag.UserId = new SelectList(db.Users.
                                            OrderBy(u=>u.FirstName).
                                            ThenBy(u=>u.LastName), "UserId", "FullName");
            var view = new AddMemberView
            {
                GroupId = groupId
            };

            return View(view);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMember(AddMemberView view)
        {
            if (!ModelState.IsValid)
            {

                ViewBag.UserId = new SelectList(db.Users.
                                            OrderBy(u => u.FirstName).
                                            ThenBy(u => u.LastName), "UserId", "FullName");
                return View(view);
            }

            var member = db.GroupMembers.Where(gm => gm.GroupId == view.GroupId && gm.UserId == view.UserId).FirstOrDefault();

            if (member != null)
            {
                ViewBag.UserId = new SelectList(db.Users.
                                            OrderBy(u => u.FirstName).
                                            ThenBy(u => u.LastName), "UserId", "FullName");
                ViewBag.Error = "The member already belongs to group.";

                return View(view);
            }

            member = new GroupMember
            { 
                GroupId = view.GroupId,
                UserId = view.UserId
            };

            db.GroupMembers.Add(member);
            db.SaveChanges();

            return RedirectToAction(string.Format("Details/{0}", view.GroupId));
        }

        // GET: Groups
        public ActionResult Index()
        {
            return View(db.Groups.ToList());
        }

        // GET: Groups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }


            var view = new GroupDetailView
            {
                GroupId = group.GroupId,
                Description = group.Description,
                Members = group.GroupMembers.ToList()
            };

            return View(view);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupId,Description")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Groups.Add(group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(group);
        }

        // GET: Groups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupId,Description")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(group);
        }

        // GET: Groups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Group group = db.Groups.Find(id);
            db.Groups.Remove(group);
            db.SaveChanges();
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
