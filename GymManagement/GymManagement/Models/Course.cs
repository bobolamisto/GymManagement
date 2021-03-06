﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GymManagement.Models
{
    public enum CourseDifficulty
    {
        All, //only for display purposes
        Easy,
        Medium,
        Hard
    }

    public class Course
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public CourseDifficulty Dificulty { get; set; }

        //antrenorii de la curs
        public virtual ICollection<UserCourse> Trainers { get; set; }

        [Display(Name = "Course type")]
        public int CourseTypeId { get; set; }
        [Display(Name = "Course type")]
        public virtual CourseType CourseType { get; set; }

        //datele in care se tine cursul
        public virtual ICollection<Scheduler> Schedulers { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}