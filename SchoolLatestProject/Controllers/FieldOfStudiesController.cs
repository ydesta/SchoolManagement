using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SchoolLatestProject.Models;

namespace SchoolLatestProject.Controllers
{
    public class FieldOfStudiesController : Controller
    {
        private SchoolManagement db = new SchoolManagement();

        // GET: FieldOfStudies
        public ActionResult Index()
        {
            return View(db.FieldOfStudies.ToList());
        }

        // GET: FieldOfStudies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FieldOfStudy fieldOfStudy = db.FieldOfStudies.Find(id);
            if (fieldOfStudy == null)
            {
                return HttpNotFound();
            }
            return View(fieldOfStudy);
        }

        // GET: FieldOfStudies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FieldOfStudies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FieldOfStudyID,Description")] FieldOfStudy fieldOfStudy)
        {
            if (ModelState.IsValid)
            {
                db.FieldOfStudies.Add(fieldOfStudy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fieldOfStudy);
        }

        // GET: FieldOfStudies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FieldOfStudy fieldOfStudy = db.FieldOfStudies.Find(id);
            if (fieldOfStudy == null)
            {
                return HttpNotFound();
            }
            return View(fieldOfStudy);
        }

        // POST: FieldOfStudies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FieldOfStudyID,Description")] FieldOfStudy fieldOfStudy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fieldOfStudy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fieldOfStudy);
        }

        // GET: FieldOfStudies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FieldOfStudy fieldOfStudy = db.FieldOfStudies.Find(id);
            if (fieldOfStudy == null)
            {
                return HttpNotFound();
            }
            return View(fieldOfStudy);
        }

        // POST: FieldOfStudies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FieldOfStudy fieldOfStudy = db.FieldOfStudies.Find(id);
            db.FieldOfStudies.Remove(fieldOfStudy);
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
