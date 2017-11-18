using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GymManagement.Models
{
    public class CourseType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int PackageId { get; set; }
        public virtual Package Package { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}