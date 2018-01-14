using GymManagement.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GymManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<CourseType> CourseTypes { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Scheduler> Schedulers { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        public DbSet<UserScheduler> UserSchedulers { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
             
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        
    }
}