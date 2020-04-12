using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CalBuild.Models;

namespace CalBuild.Controllers
{
    public class UserEventsController : Controller
    {
        private calEntities db = new calEntities();

        // GET: UserEvents
        public ActionResult Index()
        {
            var userEvents = db.UserEvents.Include(u => u.AppUser);
            return View(userEvents.ToList());
        }

        // GET: UserEvents/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserEvent userEvent = db.UserEvents.Find(id);
            if (userEvent == null)
            {
                return HttpNotFound();
            }
            return View(userEvent);
        }

        // GET: UserEvents/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.AppUsers, "Id", "Name");
            return View();
        }

        // POST: UserEvents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,EventId,Hide")] UserEvent userEvent)
        {
            if (ModelState.IsValid)
            {
                userEvent.Id = Guid.NewGuid();
                db.UserEvents.Add(userEvent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.AppUsers, "Id", "Name", userEvent.UserId);
            return View(userEvent);
        }

        // GET: UserEvents/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserEvent userEvent = db.UserEvents.Find(id);
            if (userEvent == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.AppUsers, "Id", "Name", userEvent.UserId);
            return View(userEvent);
        }

        // POST: UserEvents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,EventId,Hide")] UserEvent userEvent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userEvent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.AppUsers, "Id", "Name", userEvent.UserId);
            return View(userEvent);
        }

        // GET: UserEvents/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserEvent userEvent = db.UserEvents.Find(id);
            if (userEvent == null)
            {
                return HttpNotFound();
            }
            return View(userEvent);
        }

        // POST: UserEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            UserEvent userEvent = db.UserEvents.Find(id);
            db.UserEvents.Remove(userEvent);
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
