using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GymManagement.Models
{
    //participarile userului la planificari ale cursurilor
    public class UserScheduler
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public int SchedulerId { get; set; }
        public virtual Scheduler Scheduler { get; set; }
    }
}