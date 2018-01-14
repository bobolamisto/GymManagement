using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GymManagement.Data;
using GymManagement.Models;
using Microsoft.AspNet.Identity;

namespace GymManagement.Controllers
{
    public class ScheduleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserScheduler
        public ActionResult Index()
        {
            var model = PrepareScheduleModelList();
            return View(model);
        }

        private List<ScheduleTable> PrepareScheduleModelList()
        {
            var x = db.Schedulers.ToList();
            List<ScheduleTable> scheduleTable = new List<ScheduleTable>();
            for (var i = 8; i <= 18; i += 2)
            {
                string courseHours = (i).ToString() + " - " + (i + 2).ToString();
                ScheduleCourseModel[] scheduleCourseModels = new ScheduleCourseModel[7];
                foreach (var schedule in x)
                {
                    System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("ro-RO");
                    CultureInfo ci = CultureInfo.CurrentCulture;
                    if (ci.Calendar.GetWeekOfYear(schedule.DateTime, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) == ci.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday))
                    {
                        Course course = db.Courses.ToList().Where(c => c.Id == schedule.CourseId).FirstOrDefault();
                        var roomId = db.Courses.Where(c => c.Id == schedule.CourseId).Select(c => schedule.RoomId).FirstOrDefault();
                        var roomCapacity = db.Rooms.Where(r => r.Id == roomId).Select(r => r.NumberOfSeats).FirstOrDefault();
                        var isAvailable = db.UserSchedulers.Where(us => us.SchedulerId == schedule.Id).Count() < roomCapacity ? true : false;
                        if (schedule.DateTime.Hour >= i && schedule.DateTime.Hour < i + 2)
                        {
                            if (schedule.DateTime.DayOfWeek.ToString().Equals("Monday"))
                            {
                                scheduleCourseModels[0] = new ScheduleCourseModel
                                {
                                    courseId = course.Id,
                                    schedulerId = schedule.Id,
                                    courseName = course.Name,
                                    IsAvailable = isAvailable
                                };
                            }
                            if (schedule.DateTime.DayOfWeek.ToString().Equals("Tuesday"))
                            {
                                scheduleCourseModels[1] = new ScheduleCourseModel
                                {
                                    courseId = course.Id,
                                    schedulerId = schedule.Id,
                                    courseName = course.Name,
                                    IsAvailable = isAvailable
                                };
                            }
                            if (schedule.DateTime.DayOfWeek.ToString().Equals("Wednesday"))
                            {
                                scheduleCourseModels[2] = new ScheduleCourseModel
                                {
                                    courseId = course.Id,
                                    schedulerId = schedule.Id,
                                    courseName = course.Name,
                                    IsAvailable = isAvailable
                                };
                            }
                            if (schedule.DateTime.DayOfWeek.ToString().Equals("Thursday"))
                            {
                                scheduleCourseModels[3] = new ScheduleCourseModel
                                {
                                    courseId = course.Id,
                                    schedulerId = schedule.Id,
                                    courseName = course.Name,
                                    IsAvailable = isAvailable
                                };
                            }
                            if (schedule.DateTime.DayOfWeek.ToString().Equals("Friday"))
                            {
                                scheduleCourseModels[4] = new ScheduleCourseModel
                                {
                                    courseId = course.Id,
                                    schedulerId = schedule.Id,
                                    courseName = course.Name,
                                    IsAvailable = isAvailable
                                };
                            }
                            if (schedule.DateTime.DayOfWeek.ToString().Equals("Saturday"))
                            {
                                scheduleCourseModels[5] = new ScheduleCourseModel
                                {
                                    courseId = course.Id,
                                    schedulerId = schedule.Id,
                                    courseName = course.Name,
                                    IsAvailable = isAvailable
                                };
                            }
                            if (schedule.DateTime.DayOfWeek.ToString().Equals("Sunday"))
                            {
                                scheduleCourseModels[6] = new ScheduleCourseModel
                                {
                                    courseId = course.Id,
                                    schedulerId = schedule.Id,
                                    courseName = course.Name,
                                    IsAvailable = isAvailable
                                };
                            }
                        }


                    }
                }
                scheduleTable.Add(new ScheduleTable(courseHours, scheduleCourseModels));

            }
            return scheduleTable;
        }

        [HttpPost]
        public void Create(int schedulerId)
        {

            var userId = User.Identity.GetUserId();
            var asdasd = db.UserSchedulers.Where(us => us.UserId == userId && us.SchedulerId == schedulerId).FirstOrDefault();
            if (db.UserSchedulers.Where(us => us.UserId == userId && us.SchedulerId == schedulerId).FirstOrDefault() == null)
            {
                var userScheduler = new UserScheduler();
                userScheduler.UserId = userId;
                userScheduler.SchedulerId = schedulerId;

                db.UserSchedulers.Add(userScheduler);
                db.SaveChanges();
            }
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
