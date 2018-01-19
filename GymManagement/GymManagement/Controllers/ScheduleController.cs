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
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("ro-RO");
            CultureInfo ci = CultureInfo.CurrentCulture;
            var currentWeek = ci.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return RedirectToAction("Week/"+ currentWeek);

        }

        public ActionResult Week(int id)
        {
            var model = PrepareScheduleModelList(id);
            return View(model);
        }

        private List<ScheduleTable> PrepareScheduleModelList(int week)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("ro-RO");
            CultureInfo ci = CultureInfo.CurrentCulture;
            var x = db.Schedulers.ToList();
            List<ScheduleTable> scheduleTable = new List<ScheduleTable>();

            var currentWeek = ci.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            var diff = (week - currentWeek) * 7;
            var firstDay = DateTime.Now.AddDays(diff);
            var dayOfWeek = (int)firstDay.DayOfWeek;
            diff -= dayOfWeek - 1;
            firstDay = DateTime.Now.AddDays(diff);
            ViewData["currentWeek"] = firstDay.ToShortDateString() + " - " + firstDay.AddDays(6).ToShortDateString();

            var dificulty = Request.Form["CourseDificulty"];
            if (dificulty == null)
                ViewData["selectedDifficulty"] = CourseDifficulty.All;
            else
                ViewData["selectedDifficulty"] = (CourseDifficulty)Enum.Parse(typeof(CourseDifficulty), dificulty);
            for (var i = 8; i <= 18; i += 2)
            {
                string courseHours = (i).ToString() + " - " + (i + 2).ToString();
                ScheduleCourseModel[] scheduleCourseModels = new ScheduleCourseModel[7];
                foreach (var schedule in x)
                {


                    if (ci.Calendar.GetWeekOfYear(schedule.DateTime, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) == week)
                    {
                        Course course = db.Courses.ToList().Where(c => c.Id == schedule.CourseId).FirstOrDefault();
                        if (dificulty == null || dificulty == "All" || course.Dificulty.ToString() == dificulty)
                        {
                            var roomId = db.Courses.Where(c => c.Id == schedule.CourseId).Select(c => schedule.RoomId).FirstOrDefault();
                            var roomName = db.Courses.Where(c => c.Id == schedule.CourseId).Include(c=> schedule.Room).Select(c => schedule.Room.Name).FirstOrDefault();
                            var roomCapacity = db.Rooms.Where(r => r.Id == roomId).Select(r => r.NumberOfSeats).FirstOrDefault();
                            var bookedSeats = db.UserSchedulers.Where(us => us.SchedulerId == schedule.Id).Count();
                            var availableSeatsNr = roomCapacity - bookedSeats;
                            var isAvailable = bookedSeats < roomCapacity ? true : false;
                            if (schedule.DateTime.Hour >= i && schedule.DateTime.Hour < i + 2)
                            {
                                if (schedule.DateTime.DayOfWeek.ToString().Equals("Monday"))
                                {
                                    scheduleCourseModels[0] = new ScheduleCourseModel
                                    {
                                        CourseId = course.Id,
                                        SchedulerId = schedule.Id,
                                        CourseName = course.Name,
                                        IsAvailable = isAvailable,
                                        RoomName = roomName,
                                        AvailableSeatsNr = availableSeatsNr
                                    };
                                }
                                if (schedule.DateTime.DayOfWeek.ToString().Equals("Tuesday"))
                                {
                                    scheduleCourseModels[1] = new ScheduleCourseModel
                                    {
                                        CourseId = course.Id,
                                        SchedulerId = schedule.Id,
                                        CourseName = course.Name,
                                        IsAvailable = isAvailable,
                                        RoomName = roomName,
                                        AvailableSeatsNr = availableSeatsNr
                                    };
                                }
                                if (schedule.DateTime.DayOfWeek.ToString().Equals("Wednesday"))
                                {
                                    scheduleCourseModels[2] = new ScheduleCourseModel
                                    {
                                        CourseId = course.Id,
                                        SchedulerId = schedule.Id,
                                        CourseName = course.Name,
                                        IsAvailable = isAvailable,
                                        RoomName = roomName,
                                        AvailableSeatsNr = availableSeatsNr
                                    };
                                }
                                if (schedule.DateTime.DayOfWeek.ToString().Equals("Thursday"))
                                {
                                    scheduleCourseModels[3] = new ScheduleCourseModel
                                    {
                                        CourseId = course.Id,
                                        SchedulerId = schedule.Id,
                                        CourseName = course.Name,
                                        IsAvailable = isAvailable,
                                        RoomName = roomName,
                                        AvailableSeatsNr = availableSeatsNr
                                    };
                                }
                                if (schedule.DateTime.DayOfWeek.ToString().Equals("Friday"))
                                {
                                    scheduleCourseModels[4] = new ScheduleCourseModel
                                    {
                                        CourseId = course.Id,
                                        SchedulerId = schedule.Id,
                                        CourseName = course.Name,
                                        IsAvailable = isAvailable,
                                        RoomName = roomName,
                                        AvailableSeatsNr = availableSeatsNr
                                    };
                                }
                                if (schedule.DateTime.DayOfWeek.ToString().Equals("Saturday"))
                                {
                                    scheduleCourseModels[5] = new ScheduleCourseModel
                                    {
                                        CourseId = course.Id,
                                        SchedulerId = schedule.Id,
                                        CourseName = course.Name,
                                        IsAvailable = isAvailable,
                                        RoomName = roomName,
                                        AvailableSeatsNr = availableSeatsNr
                                    };
                                }
                                if (schedule.DateTime.DayOfWeek.ToString().Equals("Sunday"))
                                {
                                    scheduleCourseModels[6] = new ScheduleCourseModel
                                    {
                                        CourseId = course.Id,
                                        SchedulerId = schedule.Id,
                                        CourseName = course.Name,
                                        IsAvailable = isAvailable,
                                        RoomName = roomName,
                                        AvailableSeatsNr = availableSeatsNr
                                    };
                                }
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
