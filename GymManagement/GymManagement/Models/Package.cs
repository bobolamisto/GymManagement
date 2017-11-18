using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GymManagement.Models
{
    public class Package
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        //pachetul include mai multe tipuri de cursuri
        public virtual ICollection<CourseType> CourseTypes { get; set; }

    }
}