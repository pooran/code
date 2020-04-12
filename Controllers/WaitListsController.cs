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
    public class WaitListsController : Controller
    {
        private calEntities db = new calEntities();

        // GET: WaitLists
        public ActionResult Index()
        {
            var waitLists = db.WaitLists.Include(w => w.Event);
            return View(waitLists.ToList());
        }

        // GET: WaitLists/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WaitList waitList = db.WaitLists.Find(id);
            if (waitList == null)
            {
                return HttpNotFound();
            }
            return View(waitList);
        }

        // GET: WaitLists/Create
        public ActionResult Create()
        {
            ViewBag.EventId = new SelectList(db.Events, "Id", "Name");
            return View();
        }

        // POST: WaitLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EventId,UserId,IsActive")] WaitList waitList)
        {
            if (ModelState.IsValid)
            {
                waitList.Id = Guid.NewGuid();
                db.WaitLists.Add(waitList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventId = new SelectList(db.Events, "Id", "Name", waitList.EventId);
            return View(waitList);
        }

        // GET: WaitLists/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WaitList waitList = db.WaitLists.Find(id);
            if (waitList == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventId = new SelectList(db.Events, "Id", "Name", waitList.EventId);
            return View(waitList);
        }

        // POST: WaitLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EventId,UserId,IsActive")] WaitList waitList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(waitList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventId = new SelectList(db.Events, "Id", "Name", waitList.EventId);
            return View(waitList);
        }

        // GET: WaitLists/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WaitList waitList = db.WaitLists.Find(id);
            if (waitList == null)
            {
                return HttpNotFound();
            }
            return View(waitList);
        }

        // POST: WaitLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            WaitList waitList = db.WaitLists.Find(id);
            db.WaitLists.Remove(waitList);
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
