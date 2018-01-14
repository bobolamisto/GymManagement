using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using GymManagement.Data;
using GymManagement.Models;
using Microsoft.AspNet.Identity;

namespace GymManagement.Controllers
{
    public class UserSchedulerController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        private int GetWeekOfYear(DateTime time)
        {
            DateTime d = DateTime.Now;
            CultureInfo cul = CultureInfo.CurrentCulture;
            return cul.Calendar.GetWeekOfYear(
                d,
                CalendarWeekRule.FirstDay,
                DayOfWeek.Monday);
        }

        private bool AreFallingInSameWeek(DateTime d1, DateTime d2)
        {
            if (GetWeekOfYear(d1) == GetWeekOfYear(d2))
            {
                return true;
            }
            return false;
        }

        public List<ScheduleTable> Courses()
        {
            List<ScheduleTable> scheduleTable = new List<ScheduleTable>();
            var x = db.UserSchedulers.Include(u => u.Scheduler).Include(u => u.User).ToList();
            var userSchedulers = x.Where(u => User.Identity.GetUserId() == u.User.Id).ToList();
            for (var i = 8; i <= 18; i += 2)
            {
                String courseHours = (i).ToString() + " - " + (i + 2).ToString();
                String[] courseName = new String[7];
                foreach (var schedule in userSchedulers)
                {
                    Course course = db.Courses.ToList().Where(c => c.Id == schedule.Scheduler.CourseId).FirstOrDefault();

                    if (schedule.Scheduler.DateTime.Hour >= i && schedule.Scheduler.DateTime.Hour < i + 2)
                    {
                        if (schedule.Scheduler.DateTime.DayOfWeek.ToString().Equals("Monday") && AreFallingInSameWeek(schedule.Scheduler.DateTime, DateTime.Now))
                        {
                            courseName[0] = course.Name + " (" + course.Dificulty.ToString() + ")";
                        }
                        if (schedule.Scheduler.DateTime.DayOfWeek.ToString().Equals("Tuesday") && AreFallingInSameWeek(schedule.Scheduler.DateTime, DateTime.Now))
                        {
                            courseName[1] = course.Name + " (" + course.Dificulty.ToString() + ")";
                        }
                        if (schedule.Scheduler.DateTime.DayOfWeek.ToString().Equals("Wednesday") && AreFallingInSameWeek(schedule.Scheduler.DateTime, DateTime.Now))
                        {
                            courseName[2] = course.Name + " (" + course.Dificulty.ToString() + ")";
                        }
                        if (schedule.Scheduler.DateTime.DayOfWeek.ToString().Equals("Thursday") && AreFallingInSameWeek(schedule.Scheduler.DateTime, DateTime.Now))
                        {
                            courseName[3] = course.Name + " (" + course.Dificulty.ToString() + ")";
                        }
                        if (schedule.Scheduler.DateTime.DayOfWeek.ToString().Equals("Friday")
                        && AreFallingInSameWeek(schedule.Scheduler.DateTime, DateTime.Now))
                        {
                            courseName[4] = course.Name + " (" + course.Dificulty.ToString() + ")";
                        }
                        if (schedule.Scheduler.DateTime.DayOfWeek.ToString().Equals("Saturday") &&
                        AreFallingInSameWeek(schedule.Scheduler.DateTime, DateTime.Now))
                        {
                            courseName[5] = course.Name + " (" + course.Dificulty.ToString() + ")";
                        }
                        if (schedule.Scheduler.DateTime.DayOfWeek.ToString().Equals("Sunday") &&
                        AreFallingInSameWeek(schedule.Scheduler.DateTime, DateTime.Now))
                        {
                            courseName[6] = course.Name + " (" + course.Dificulty.ToString() + ")";
                        }
                    }

                }

                scheduleTable.Add(new ScheduleTable(courseHours, courseName));

            }
            return scheduleTable;
        }

        // GET: UserScheduler
        public ActionResult Index()
        {

            return View(Courses());
        }

        // GET: UserScheduler/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserScheduler userScheduler = db.UserSchedulers.Find(id);
            if (userScheduler == null)
            {
                return HttpNotFound();
            }
            return View(userScheduler);
        }

        // GET: UserScheduler/Create
        public ActionResult Create()
        {
            ViewBag.SchedulerId = new SelectList(db.Schedulers, "Id", "Id");
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: UserScheduler/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,SchedulerId")] UserScheduler userScheduler)
        {
            if (ModelState.IsValid)
            {
                db.UserSchedulers.Add(userScheduler);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SchedulerId = new SelectList(db.Schedulers, "Id", "Id", userScheduler.SchedulerId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", userScheduler.UserId);
            return View(userScheduler);
        }

        // GET: UserScheduler/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserScheduler userScheduler = db.UserSchedulers.Find(id);
            if (userScheduler == null)
            {
                return HttpNotFound();
            }
            ViewBag.SchedulerId = new SelectList(db.Schedulers, "Id", "Id", userScheduler.SchedulerId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", userScheduler.UserId);
            return View(userScheduler);
        }

        // POST: UserScheduler/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,SchedulerId")] UserScheduler userScheduler)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userScheduler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SchedulerId = new SelectList(db.Schedulers, "Id", "Id", userScheduler.SchedulerId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", userScheduler.UserId);
            return View(userScheduler);
        }

        // GET: UserScheduler/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserScheduler userScheduler = db.UserSchedulers.Find(id);
            if (userScheduler == null)
            {
                return HttpNotFound();
            }
            return View(userScheduler);
        }

        // POST: UserScheduler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserScheduler userScheduler = db.UserSchedulers.Find(id);
            db.UserSchedulers.Remove(userScheduler);
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
