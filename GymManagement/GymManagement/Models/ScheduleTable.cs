using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymManagement.Models
{
    public class ScheduleTable
    {
        public string Time;
        public string[] coursesName = new string[7];

        public ScheduleTable(string time, string[] coursesName)
        {
            Time = time;
            this.coursesName = coursesName;
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
}