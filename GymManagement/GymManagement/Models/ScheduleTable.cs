using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymManagement.Models
{
    public class ScheduleTable
    {
        public string Time;
        public ScheduleCourseModel[] scheduleCourseModels = new ScheduleCourseModel[7];
        public UserScheduleModel[] userScheduleModels = new UserScheduleModel[7];
        public string[] coursesName = new string[7];
        public int Week { get; set; }

        public ScheduleTable(string time, string[] coursesName)
        {
            Time = time;
            this.coursesName = coursesName;
        }
        public ScheduleTable(string time, ScheduleCourseModel[] scheduleCourseModels)
        {
            Time = time;
            this.scheduleCourseModels = scheduleCourseModels;
        }

        public ScheduleTable(string time, UserScheduleModel[] userScheduleModels)
        {
            Time = time;
            this.userScheduleModels = userScheduleModels;
        }

        public ScheduleTable()
        {
            Time = "";
            for (var i = 0; i < 7; i++)
            {
                coursesName[i] = "";
            }
        }
    }

    public class ScheduleCourseModel
    {
        public int courseId { get; set; }
        public int schedulerId { get; set; }
        public string courseName { get; set; }
        public bool IsAvailable { get; set; }
    }

    public class UserScheduleModel
    {
        public int courseId { get; set; }
        public int userScheduleId { get; set; }
        public string courseName { get; set; }
    }
}