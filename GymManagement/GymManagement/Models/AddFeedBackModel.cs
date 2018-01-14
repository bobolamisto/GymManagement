using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymManagement.Models
{
    public class AddFeedBackModel
    {
        public string Text { get; set; }
        public string TrainerId { get; set; }
        public int? CourseId { get; set; }
    }
}