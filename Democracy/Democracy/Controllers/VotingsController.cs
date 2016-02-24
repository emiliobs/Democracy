﻿using System;
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
    [Authorize]
    public class VotingsController : Controller
    {
        private DemocracyContext db = new DemocracyContext();

        [HttpGet]         
        public ActionResult AddGroup(int id)
        {
            ViewBag.GroupId = new SelectList(db.Groups.OrderBy(g => g.Description), "GroupId", "Description").ToList();
            
            // ViewBag.GroupId = new SelectList(db.Groups.OrderBy(vg => vg.Description), "GroupId", "Description").FirstOrDefault();
            var view = new AddGroupView
            {
                VotingId = id,
                
            };

            return View(view);
        }

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddGroup(AddGroupView view)
        {
            

            if (ModelState.IsValid)
            {
                var votingGroup = db.VotingGroups.Where(vg => vg.VotingId == view.VotingId && vg.GroupId == view.GroupId).FirstOrDefault();

                if (votingGroup != null)
                {
                    ViewBag.Error = "The group already belongs to voting...";

                    ViewBag.GroupId = new SelectList(db.Groups.OrderBy(g => g.Description), "GroupId", "Description").ToList();
                    
                    return View(view);
                }

                votingGroup = new VotingGroup
                {
                  GroupId = view.GroupId,
                  VotingId = view.VotingId,
                };

                db.VotingGroups.Add(votingGroup);
                db.SaveChanges();

                return RedirectToAction(string.Format("Details/{0}", view.VotingId));
                             
            }

            ViewBag.GroupId = new SelectList(db.Groups.OrderBy(g => g.Description), "GroupId", "Description").ToList();

            return View(view);
        }

        // GET: Votings
        public ActionResult Index()
        {
            var votings = db.Votings.Include(v => v.State);
            return View(votings.ToList());
        }

        // GET: Votings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voting voting = db.Votings.Find(id);
            if (voting == null)
            {
                return HttpNotFound();
            }
            return View(voting);
        }

        // GET: Votings/Create
        public ActionResult Create()
        {
            ViewBag.StateId = new SelectList(db.States, "StateId", "Description");

            var view = new VotingView
            {
                DateStart = DateTime.Now,
                DateEnd = DateTime.Now,
                
            };

            return View(view);
        }

        // POST: Votings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VotingView view)
        {
            if (ModelState.IsValid)
            {
                //crear objeto voting con relacaión a VotingView:
                var voting = new Voting
                {
                    DateTimeStart = view.TimeStart.AddHours(view.TimeStart.Hour).AddMinutes(view.TimeStart.Minute),
                    DateTimeEnd   = view.TimeEnd.AddHours(view.TimeEnd.Hour).AddMinutes(view.TimeEnd.Minute),
                    Description = view.Description,
                    IsEnabledBlankVote = view.IsEnabledBlankVote,
                    IsForAllUsers = view.IsForAllUsers,
                    Remarks = view.Remarks,
                    StateId = view.StateId,


                };

                db.Votings.Add(voting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StateId = new SelectList(db.States, "StateId", "Description", view.StateId);
            return View(view);
        }

        // GET: Votings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voting voting = db.Votings.Find(id);
            if (voting == null)
            {
                return HttpNotFound();
            }

            var view = new VotingView
            {
              DateEnd = voting.DateTimeEnd,
              DateStart = voting.DateTimeStart,
              Description = voting.Description,
              IsEnabledBlankVote = voting.IsEnabledBlankVote,
              IsForAllUsers = voting.IsForAllUsers,
              Remarks = voting.Remarks,
              StateId = voting.StateId,
              TimeEnd = voting.DateTimeEnd,
              TimeStart = voting.DateTimeStart,
              VotingId = voting.VotingId,
            };

            ViewBag.StateId = new SelectList(db.States, "StateId", "Description", voting.StateId);
            return View(view);
        }

        // POST: Votings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VotingView view)
        {
            if (ModelState.IsValid)
            {

                //crear objeto voting con relacaión a VotingView:
                var voting = new Voting
                {
                    VotingId = view.VotingId,
                    DateTimeStart = view.TimeStart.AddHours(view.TimeStart.Hour).AddMinutes(view.TimeStart.Minute),
                    DateTimeEnd = view.TimeEnd.AddHours(view.TimeEnd.Hour).AddMinutes(view.TimeEnd.Minute),
                    Description = view.Description,
                    IsEnabledBlankVote = view.IsEnabledBlankVote,
                    IsForAllUsers = view.IsForAllUsers,
                    Remarks = view.Remarks,
                    StateId = view.StateId,


                };

                db.Entry(voting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StateId = new SelectList(db.States, "StateId", "Description", view.StateId);
            return View(view);
        }

        // GET: Votings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voting voting = db.Votings.Find(id);
            if (voting == null)
            {
                return HttpNotFound();
            }
            return View(voting);
        }

        // POST: Votings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Voting voting = db.Votings.Find(id);
            db.Votings.Remove(voting);
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
