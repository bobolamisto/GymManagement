using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GymManagement.Models
{
    public class Scheduler
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }

        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        public int RoomId { get; set; }
        public virtual Room Room { get; set; }

        //participantii la aceasta planificare
        public virtual ICollection<UserScheduler> Users { get; set; }
    }
}