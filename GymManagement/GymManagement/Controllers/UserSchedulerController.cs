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
    [Authorize]
    public class UserSchedulerController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        private EmailService emailService = new EmailService();


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
            var dificulty = Request.Form["CourseDificulty"];
            if (dificulty == null)
                ViewData["selectedDifficulty"] = CourseDifficulty.All;
            else
                ViewData["selectedDifficulty"] = (CourseDifficulty)Enum.Parse(typeof(CourseDifficulty), dificulty);

            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("ro-RO");
            CultureInfo ci = CultureInfo.CurrentCulture;
            var week = (int)TempData["currentWeekNumber"];
            var currentWeek = ci.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            var diff = (week - currentWeek) * 7;
            var firstDay = DateTime.Now.AddDays(diff);
            var dayOfWeek = (int)firstDay.DayOfWeek;
            diff -= dayOfWeek - 1;
            firstDay = DateTime.Now.AddDays(diff);
            ViewData["currentWeek"] = firstDay.ToShortDateString() + " - " + firstDay.AddDays(6).ToShortDateString();

            for (var i = 8; i <= 18; i += 2)
            {
                String courseHours = (i).ToString() + " - " + (i + 2).ToString();
                UserScheduleModel[] userScheduleModels = new UserScheduleModel[7];
                foreach (var schedule in userSchedulers)
                {
                    Course course = db.Courses.ToList().Where(c => c.Id == schedule.Scheduler.CourseId).FirstOrDefault();
                    if ((dificulty == "All" || dificulty == null || course.Dificulty.ToString() == dificulty) &&
                        ci.Calendar.GetWeekOfYear(schedule.Scheduler.DateTime, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) == week)
                    {

                        if (schedule.Scheduler.DateTime.Hour >= i && schedule.Scheduler.DateTime.Hour < i + 2)
                        {
                            if (schedule.Scheduler.DateTime.DayOfWeek.ToString().Equals("Monday") && AreFallingInSameWeek(schedule.Scheduler.DateTime, DateTime.Now))
                            {
                                userScheduleModels[0] = new UserScheduleModel
                                {
                                    courseId = course.Id,
                                    userScheduleId = schedule.Id,
                                    courseName = course.Name + " (" + course.Dificulty.ToString() + ")"
                                };
                            }
                            if (schedule.Scheduler.DateTime.DayOfWeek.ToString().Equals("Tuesday") && AreFallingInSameWeek(schedule.Scheduler.DateTime, DateTime.Now))
                            {
                                userScheduleModels[1] = new UserScheduleModel
                                {
                                    courseId = course.Id,
                                    userScheduleId = schedule.Id,
                                    courseName = course.Name + " (" + course.Dificulty.ToString() + ")"
                                };
                            }
                            if (schedule.Scheduler.DateTime.DayOfWeek.ToString().Equals("Wednesday") && AreFallingInSameWeek(schedule.Scheduler.DateTime, DateTime.Now))
                            {
                                userScheduleModels[2] = new UserScheduleModel
                                {
                                    courseId = course.Id,
                                    userScheduleId = schedule.Id,
                                    courseName = course.Name + " (" + course.Dificulty.ToString() + ")"
                                };
                            }
                            if (schedule.Scheduler.DateTime.DayOfWeek.ToString().Equals("Thursday") && AreFallingInSameWeek(schedule.Scheduler.DateTime, DateTime.Now))
                            {
                                userScheduleModels[3] = new UserScheduleModel
                                {
                                    courseId = course.Id,
                                    userScheduleId = schedule.Id,
                                    courseName = course.Name + " (" + course.Dificulty.ToString() + ")"
                                };
                            }
                            if (schedule.Scheduler.DateTime.DayOfWeek.ToString().Equals("Friday")
                            && AreFallingInSameWeek(schedule.Scheduler.DateTime, DateTime.Now))
                            {
                                userScheduleModels[4] = new UserScheduleModel
                                {
                                    courseId = course.Id,
                                    userScheduleId = schedule.Id,
                                    courseName = course.Name + " (" + course.Dificulty.ToString() + ")"
                                };
                            }
                            if (schedule.Scheduler.DateTime.DayOfWeek.ToString().Equals("Saturday") &&
                            AreFallingInSameWeek(schedule.Scheduler.DateTime, DateTime.Now))
                            {
                                userScheduleModels[5] = new UserScheduleModel
                                {
                                    courseId = course.Id,
                                    userScheduleId = schedule.Id,
                                    courseName = course.Name + " (" + course.Dificulty.ToString() + ")"
                                };
                            }
                            if (schedule.Scheduler.DateTime.DayOfWeek.ToString().Equals("Sunday") &&
                            AreFallingInSameWeek(schedule.Scheduler.DateTime, DateTime.Now))
                            {
                                userScheduleModels[6] = new UserScheduleModel
                                {
                                    courseId = course.Id,
                                    userScheduleId = schedule.Id,
                                    courseName = course.Name + " (" + course.Dificulty.ToString() + ")"
                                };
                            }
                        }

                    }
                }

                scheduleTable.Add(new ScheduleTable(courseHours, userScheduleModels));

            }
            return scheduleTable;
        }

        // GET: UserScheduler
        public ActionResult Index()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("ro-RO");
            CultureInfo ci = CultureInfo.CurrentCulture;
            if (TempData["currentWeekNumber"] == null)
            {
                TempData["currentWeekNumber"] = ci.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            }
            return View(Courses());
        }

        public ActionResult SetWeek(int id)
        {
            TempData["currentWeekNumber"] = id;
            
            return RedirectToAction("Index");
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
        public ActionResult DeleteConfirmed(int userScheduleId)
        {
            var userId = User.Identity.GetUserId();
            var userSchedule = db.UserSchedulers.Where(us => us.UserId == userId && us.Id == userScheduleId).FirstOrDefault();
            if (userSchedule != null)
            {
                db.UserSchedulers.Remove(userSchedule);
                db.SaveChanges();
            }
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
