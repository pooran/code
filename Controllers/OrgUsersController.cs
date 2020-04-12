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
    public class OrgUsersController : Controller
    {
        private calEntities db = new calEntities();

        // GET: OrgUsers
        public ActionResult Index()
        {
            var orgUsers = db.OrgUsers.Include(o => o.AppUser).Include(o => o.Group).Include(o => o.Org);
            return View(orgUsers.ToList());
        }

        // GET: OrgUsers/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrgUser orgUser = db.OrgUsers.Find(id);
            if (orgUser == null)
            {
                return HttpNotFound();
            }
            return View(orgUser);
        }

        // GET: OrgUsers/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.AppUsers, "Id", "Name");
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name");
            ViewBag.OrgId = new SelectList(db.Orgs, "Id", "Name");
            return View();
        }

        // POST: OrgUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrgId,GroupId,UserId,CreatedDate,ModifiedDate,IsActive")] OrgUser orgUser)
        {
            if (ModelState.IsValid)
            {
                orgUser.Id = Guid.NewGuid();
                db.OrgUsers.Add(orgUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.AppUsers, "Id", "Name", orgUser.UserId);
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name", orgUser.GroupId);
            ViewBag.OrgId = new SelectList(db.Orgs, "Id", "Name", orgUser.OrgId);
            return View(orgUser);
        }

        // GET: OrgUsers/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrgUser orgUser = db.OrgUsers.Find(id);
            if (orgUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.AppUsers, "Id", "Name", orgUser.UserId);
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name", orgUser.GroupId);
            ViewBag.OrgId = new SelectList(db.Orgs, "Id", "Name", orgUser.OrgId);
            return View(orgUser);
        }

        // POST: OrgUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrgId,GroupId,UserId,CreatedDate,ModifiedDate,IsActive")] OrgUser orgUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orgUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.AppUsers, "Id", "Name", orgUser.UserId);
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name", orgUser.GroupId);
            ViewBag.OrgId = new SelectList(db.Orgs, "Id", "Name", orgUser.OrgId);
            return View(orgUser);
        }

        // GET: OrgUsers/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrgUser orgUser = db.OrgUsers.Find(id);
            if (orgUser == null)
            {
                return HttpNotFound();
            }
            return View(orgUser);
        }

        // POST: OrgUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            OrgUser orgUser = db.OrgUsers.Find(id);
            db.OrgUsers.Remove(orgUser);
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
