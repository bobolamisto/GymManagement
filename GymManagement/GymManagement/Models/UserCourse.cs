using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GymManagement.Models
{
    // Aici retinem legatura dintre antrenori si cursuri; participantii la un curs se retin in legatura User - Scheduler
    public class UserCourse
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int CourseId { get; set; }
        public virtual User User { get; set; }
        public virtual Course Course { get; set; }
    }
}