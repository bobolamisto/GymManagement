using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GymManagement.Data;
using GymManagement.Models;

namespace GymManagement.Controllers
{
    public class CoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Courses
        public ActionResult Index()
        {

            var courses = db.Courses.Include(c => c.CourseType);

            var dificulty = Request.Form["CourseDificulty"];
            CourseDificulty selectedDificulty;
            if (dificulty != null)
            {
                if (dificulty == "Easy")
                {
                    selectedDificulty = CourseDificulty.Easy;
                }
                else if (dificulty == "Medium")
                {
                    selectedDificulty = CourseDificulty.Medium;
                }
                else
                {
                    selectedDificulty = CourseDificulty.Hard;
                }
                courses = courses.Where(c => c.Dificulty == selectedDificulty);
            }
            return View(courses.ToList());
        }
       


        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // CourseViewModel mymodel = new CourseViewModel();
            //mymodel.course = db.Courses.Include(c => c.Trainers);
            // mymodel.Students = GetStudents();
            //return View(mymodel);
            Course course = db.Courses.Include(c => c.Trainers).FirstOrDefault(c => c.Id == id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            ViewBag.CourseTypeId = new SelectList(db.CourseTypes, "Id", "Name");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CourseTypeId")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseTypeId = new SelectList(db.CourseTypes, "Id", "Name", course.CourseTypeId);
            return View(course);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseTypeId = new SelectList(db.CourseTypes, "Id", "Name", course.CourseTypeId);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CourseTypeId")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseTypeId = new SelectList(db.CourseTypes, "Id", "Name", course.CourseTypeId);
            return View(course);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
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

        [HttpPost]
        public ActionResult AddFeedbackToCourse(AddFeedBackModel model)
        {
            var customer = db.Users.FirstOrDefault(u => u.Email == User.Identity.Name).FullName;
            var feedback = new Feedback { Text = model.Text, Date = DateTime.Now, UserFullName = customer, CourseId = model.CourseId };
            db.Feedbacks.Add(feedback);
            db.SaveChanges();
            //return RedirectToAction("TrainerDetails", new { id = model.TrainerId });
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}
