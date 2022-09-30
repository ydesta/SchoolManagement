using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SchoolLatestProject.Models;
using PagedList;
using PagedList.Mvc;
namespace SchoolLatestProject.Controllers
{
    public class MarkListsController : Controller
    {
        private SchoolManagement db = new SchoolManagement();

        // GET: MarkLists
        public ActionResult Index(int? page)
        {

            var markLists = db.MarkLists.Include(m => m.TblCours).Include(m => m.Student);
            return View(markLists.ToList().ToPagedList(page ?? 1,5));
        }

        // GET: MarkLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarkList markList = db.MarkLists.Find(id);
            if (markList == null)
            {
                return HttpNotFound();
            }
            return View(markList);
        }

        // GET: MarkLists/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.TblCourses, "CourseID", "CourseName");
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName");
            return View();
        }

        // POST: MarkLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MarkListID,StudentID,CourseID,Sesmester,AcademicYear,Mark")] MarkList markList)
        {
            if (ModelState.IsValid)
            {
                db.MarkLists.Add(markList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.TblCourses, "CourseID", "CourseName", markList.CourseID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", markList.StudentID);
            return View(markList);
        }

        // GET: MarkLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarkList markList = db.MarkLists.Find(id);
            if (markList == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.TblCourses, "CourseID", "CourseName", markList.CourseID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", markList.StudentID);
            return View(markList);
        }

        // POST: MarkLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MarkListID,StudentID,CourseID,Sesmester,AcademicYear,Mark")] MarkList markList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(markList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.TblCourses, "CourseID", "CourseName", markList.CourseID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", markList.StudentID);
            return View(markList);
        }

        // GET: MarkLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarkList markList = db.MarkLists.Find(id);
            if (markList == null)
            {
                return HttpNotFound();
            }
            return View(markList);
        }

        // POST: MarkLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MarkList markList = db.MarkLists.Find(id);
            db.MarkLists.Remove(markList);
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
