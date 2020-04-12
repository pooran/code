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
    public class EventSlotsController : Controller
    {
        private calEntities db = new calEntities();

        // GET: EventSlots
        public ActionResult Index()
        {
            var eventSlots = db.EventSlots.Include(e => e.Event);
            return View(eventSlots.ToList());
        }

        // GET: EventSlots/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventSlot eventSlot = db.EventSlots.Find(id);
            if (eventSlot == null)
            {
                return HttpNotFound();
            }
            return View(eventSlot);
        }

        // GET: EventSlots/Create
        public ActionResult Create()
        {
            ViewBag.EventId = new SelectList(db.Events, "Id", "Name");
            return View();
        }

        // POST: EventSlots/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EventId,StartTime,EndTime,CreatedTime,ModifiedTime,BookedBy,IsActive")] EventSlot eventSlot)
        {
            if (ModelState.IsValid)
            {
                eventSlot.Id = Guid.NewGuid();
                db.EventSlots.Add(eventSlot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventId = new SelectList(db.Events, "Id", "Name", eventSlot.EventId);
            return View(eventSlot);
        }

        // GET: EventSlots/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventSlot eventSlot = db.EventSlots.Find(id);
            if (eventSlot == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventId = new SelectList(db.Events, "Id", "Name", eventSlot.EventId);
            return View(eventSlot);
        }

        // POST: EventSlots/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EventId,StartTime,EndTime,CreatedTime,ModifiedTime,BookedBy,IsActive")] EventSlot eventSlot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventSlot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventId = new SelectList(db.Events, "Id", "Name", eventSlot.EventId);
            return View(eventSlot);
        }

        // GET: EventSlots/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventSlot eventSlot = db.EventSlots.Find(id);
            if (eventSlot == null)
            {
                return HttpNotFound();
            }
            return View(eventSlot);
        }

        // POST: EventSlots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            EventSlot eventSlot = db.EventSlots.Find(id);
            db.EventSlots.Remove(eventSlot);
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
