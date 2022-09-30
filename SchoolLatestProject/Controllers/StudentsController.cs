using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SchoolLatestProject.Models;
using System.IO;
using PagedList;
using PagedList.Mvc;

namespace SchoolLatestProject.Controllers
{
    public class StudentsController : Controller
    {
        private SchoolManagement db = new SchoolManagement();

        // GET: Students
        public ActionResult Index(string SearchString,int? page, string sortBy)
        {
            ViewBag.SortNameParm = String.IsNullOrEmpty(sortBy) ? "Name desc" : "";
            ViewBag.SortGenderParm = sortBy == "Gender" ? "Gender desc" : "Gender";
            var students = db.Students.AsQueryable();
            students = students.Where(x=>x.FirstName.StartsWith(SearchString) || SearchString==null);
            switch (sortBy)
            {
                case "Name desc":
                    students = students.OrderByDescending(x => x.FirstName);
                    break;
                case "Gender desc":
                    students = students.OrderByDescending(x => x.Gender);
                    break;
                case "Gender":
                    students = students.OrderBy(x => x.Gender);
                    break;
                default:               
                    students = students.OrderBy(x => x.FirstName);
                    break;
            }
          return View(students.ToPagedList(page ?? 1, 3));
        }
        public JsonResult GetStudents(string term)
        {
            List<string> students;
            students = db.Students.Where(x => x.FirstName.StartsWith(term)).Select(y => y.FirstName).ToList();
            return Json(students, JsonRequestBehavior.AllowGet);
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {

            string fileName = Path.GetFileNameWithoutExtension(student.ImageFile.FileName);
            string extention = Path.GetExtension(student.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
            student.ImagePhath = "~/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            student.ImageFile.SaveAs(fileName);
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,FirstName,MiddleName,LastName,Gender,Grade,Section,Telephone,ParentFirstName,ParentmiddleName,ParentLastName,ParentTelephone,Subcity,Wereda,Kebelle,HouseNo,ImagePhath")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
