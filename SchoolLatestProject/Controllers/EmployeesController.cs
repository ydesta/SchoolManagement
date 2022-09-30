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

namespace SchoolLatestProject.Controllers
{
    public class EmployeesController : Controller
    {
        private SchoolManagement db = new SchoolManagement();

        // GET: Employees
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.Department).Include(e => e.EducationLevel).Include(e => e.FieldOfStudy);
            return View(employees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName");
            ViewBag.EducationLevelID = new SelectList(db.EducationLevels, "EducationLevelID", "Description");
            ViewBag.FieldOfStudyID = new SelectList(db.FieldOfStudies, "FieldOfStudyID", "Description");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            string fileName = Path.GetFileNameWithoutExtension(employee.ImageFile.FileName);
            string extention = Path.GetExtension(employee.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
            employee.ImagePath = "~/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            employee.ImageFile.SaveAs(fileName);
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "HeadDepartmentName", employee.DepartmentID);
            ViewBag.EducationLevelID = new SelectList(db.EducationLevels, "EducationLevelID", "Description", employee.EducationLevelID);
            ViewBag.FieldOfStudyID = new SelectList(db.FieldOfStudies, "FieldOfStudyID", "Description", employee.FieldOfStudyID);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "HeadDepartmentName", employee.DepartmentID);
            ViewBag.EducationLevelID = new SelectList(db.EducationLevels, "EducationLevelID", "Description", employee.EducationLevelID);
            ViewBag.FieldOfStudyID = new SelectList(db.FieldOfStudies, "FieldOfStudyID", "Description", employee.FieldOfStudyID);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeId,UserNo,FirstName,MiddleName,LastName,Gender,DateOfBirth,Position,MaritalStatus,Salary,HireDate,EmployeeType,Nationality,Religion,ImagePath,EducationLevelID,FieldOfStudyID,DepartmentID")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "HeadDepartmentName", employee.DepartmentID);
            ViewBag.EducationLevelID = new SelectList(db.EducationLevels, "EducationLevelID", "Description", employee.EducationLevelID);
            ViewBag.FieldOfStudyID = new SelectList(db.FieldOfStudies, "FieldOfStudyID", "Description", employee.FieldOfStudyID);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
