using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GymManagement.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public string UserFullName { get; set; }
        public DateTime Date { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public int? CourseId { get; set; }
        public virtual Course Course { get; set; }

    }
}