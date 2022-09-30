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
    public class AdmissionsController : Controller
    {
        private SchoolManagement db = new SchoolManagement();

        // GET: Admissions
        public ActionResult Index()
        {
            var admissions = db.Admissions.Include(a => a.Student);
            return View(admissions.ToList());
        }

        // GET: Admissions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admission admission = db.Admissions.Find(id);
            if (admission == null)
            {
                return HttpNotFound();
            }
            return View(admission);
        }

        // GET: Admissions/Create
        public ActionResult Create()
        {
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName");
            return View();
        }

        // POST: Admissions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdmissionId,StudentID,AcademicYear,Grade")] Admission admission)
        {
            if (ModelState.IsValid)
            {
                db.Admissions.Add(admission);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", admission.StudentID);
            return View(admission);
        }

        // GET: Admissions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admission admission = db.Admissions.Find(id);
            if (admission == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", admission.StudentID);
            return View(admission);
        }

        // POST: Admissions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdmissionId,StudentID,AcademicYear,Grade")] Admission admission)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admission).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", admission.StudentID);
            return View(admission);
        }

        // GET: Admissions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admission admission = db.Admissions.Find(id);
            if (admission == null)
            {
                return HttpNotFound();
            }
            return View(admission);
        }

        // POST: Admissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admission admission = db.Admissions.Find(id);
            db.Admissions.Remove(admission);
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
