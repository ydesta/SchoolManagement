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
    public class TblCoursController : Controller
    {
        private SchoolManagement db = new SchoolManagement();

        // GET: TblCours
        public ActionResult Index()
        {
            return View(db.TblCourses.ToList());
        }

        // GET: TblCours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblCours tblCours = db.TblCourses.Find(id);
            if (tblCours == null)
            {
                return HttpNotFound();
            }
            return View(tblCours);
        }

        // GET: TblCours/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TblCours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TblCours tblCours)
        {
            if (ModelState.IsValid)
            {
                db.TblCourses.Add(tblCours);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblCours);
        }

        // GET: TblCours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblCours tblCours = db.TblCourses.Find(id);
            if (tblCours == null)
            {
                return HttpNotFound();
            }
            return View(tblCours);
        }

        // POST: TblCours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TblCours tblCours)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblCours).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblCours);
        }

        // GET: TblCours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblCours tblCours = db.TblCourses.Find(id);
            if (tblCours == null)
            {
                return HttpNotFound();
            }
            return View(tblCours);
        }

        // POST: TblCours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TblCours tblCours = db.TblCourses.Find(id);
            db.TblCourses.Remove(tblCours);
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
