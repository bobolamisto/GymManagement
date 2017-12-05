namespace GymManagement.Migrations
{
    using GymManagement.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GymManagement.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GymManagement.Data.ApplicationDbContext context)
        {
            var packages = new Package[]
            {
                new Package { Id = 1, Name = "Dance for health", Price = 200},
                new Package { Id = 2, Name = "Body strength", Price = 200},
                new Package { Id = 3, Name = "Fit for life", Price = 250},
                new Package { Id = 4, Name = "Superstar for him", Price = 550},
                new Package { Id = 5, Name = "Superstar for her", Price = 500},
                new Package { Id = 6, Name = "Step & Tone", Price = 150},
                new Package { Id = 7, Name = "Functional Training", Price = 150}
            };
            context.Packages.AddOrUpdate(packages);
            context.SaveChanges();

            var courseTypes = new CourseType[]
            {
                new CourseType{ Id = 1, Name = "Zumba", PackageId = 1},
                new CourseType{ Id = 2, Name = "Kangoo Jumps", PackageId = 1},
                new CourseType{ Id = 3, Name = "Aerobic", PackageId = 1},
                new CourseType{ Id = 4, Name = "Spinning", PackageId = 2},
                new CourseType{ Id = 5, Name = "Fitness", PackageId = 2},
                new CourseType{ Id = 6, Name = "Body building", PackageId = 2},
                new CourseType{ Id = 7, Name = "Cycling", PackageId = 3},
                new CourseType{ Id = 8, Name = "Yoga", PackageId = 3},
                new CourseType{ Id = 9, Name = "Core strength", PackageId = 3},
                new CourseType{ Id = 10, Name = "Tae bo", PackageId = 4},
                new CourseType{ Id = 11, Name = "Circuit training", PackageId = 4},
                new CourseType{ Id = 12, Name = "Body pump", PackageId = 4},
                new CourseType{ Id = 13, Name = "Pilates", PackageId = 5},
                new CourseType{ Id = 14, Name = "Stretching", PackageId = 5},
                new CourseType{ Id = 15, Name = "Meditation", PackageId = 5},
                new CourseType{ Id = 16, Name = "X-treme abs", PackageId = 6},
                new CourseType{ Id = 17, Name = "Functional Exercies", PackageId = 7}

            };
            context.CourseTypes.AddOrUpdate(courseTypes);
            context.SaveChanges();

            var courses = new Course[]
            {
                new Course{ Id = 1, Name = "Zumba for begginers", Dificulty = CourseDificulty.Easy, CourseTypeId = 1},
                new Course{ Id = 2, Name = "Zumba workout", Dificulty = CourseDificulty.Medium, CourseTypeId = 1},
                new Course{ Id = 3, Name = "Zumba for life", Dificulty = CourseDificulty.Hard, CourseTypeId = 1},
                new Course{ Id = 2, Name = "Kangoo Jumps", Dificulty = CourseDificulty.Easy, CourseTypeId = 1},
                new Course{ Id = 3, Name = "Aerobic", Dificulty = CourseDificulty.Easy, CourseTypeId = 1},
                new Course{ Id = 4, Name = "Spinning", Dificulty = CourseDificulty.Easy, CourseTypeId = 2},
                new Course{ Id = 5, Name = "Fitness", Dificulty = CourseDificulty.Easy, CourseTypeId = 2},
                new Course{ Id = 6, Name = "Body building", Dificulty = CourseDificulty.Easy, CourseTypeId = 2},
                new Course{ Id = 7, Name = "Cycling", Dificulty = CourseDificulty.Easy, CourseTypeId = 3},
                new Course{ Id = 8, Name = "Yoga", Dificulty = CourseDificulty.Medium, CourseTypeId = 3},
                new Course{ Id = 9, Name = "Core strength", Dificulty = CourseDificulty.Medium, CourseTypeId = 3},
                new Course{ Id = 10, Name = "Tae bo", Dificulty = CourseDificulty.Medium, CourseTypeId = 4},
                new Course{ Id = 11, Name = "Circuit training", Dificulty = CourseDificulty.Medium, CourseTypeId = 4},
                new Course{ Id = 12, Name = "Body pump", Dificulty = CourseDificulty.Medium, CourseTypeId = 4},
                new Course{ Id = 13, Name = "Pilates", Dificulty = CourseDificulty.Medium, CourseTypeId = 5},
                new Course{ Id = 14, Name = "Stretching", Dificulty = CourseDificulty.Hard, CourseTypeId = 5},
                new Course{ Id = 15, Name = "Meditation", Dificulty = CourseDificulty.Hard, CourseTypeId = 5},
                new Course{ Id = 16, Name = "X-treme abs", Dificulty = CourseDificulty.Hard, CourseTypeId = 6},
                new Course{ Id = 17, Name = "Functional Exercies", Dificulty = CourseDificulty.Hard, CourseTypeId = 7}
            };
            context.Courses.AddOrUpdate(courses);
            context.SaveChanges();

            var addresses = new Address[]
            {
                new Address{Id = 1, Street = "Abey Road", StreetNumber = 11, Latitude = 20, Longitude = 21},
                new Address{Id = 2, Street = "New street", StreetNumber = 10, Latitude = 24, Longitude = 29},
            };
            context.Addresses.AddOrUpdate(addresses);
            context.SaveChanges();

            var rooms = new Room[]
            {
                new Room{ Id = 1, Name = "A", NumberOfSeats = 20, AddressId = 1},
                new Room{ Id = 2, Name = "B", NumberOfSeats = 21, AddressId = 2}
            };
            context.Rooms.AddOrUpdate(rooms);
            context.SaveChanges();

            var schedulers = new Scheduler[]
            {
                new Scheduler{ Id = 1, DateTime = new DateTime(2017,12,4,12,0,0), RoomId = 1, CourseId = 1},
                new Scheduler{ Id = 2, DateTime = new DateTime(2017,12,5,14,0,0), RoomId = 1, CourseId = 2},
                new Scheduler{ Id = 3, DateTime = new DateTime(2017,12,6,16,0,0), RoomId = 1, CourseId = 3},
                new Scheduler{ Id = 4, DateTime = new DateTime(2017,12,7,18,0,0), RoomId = 1, CourseId = 4},
                new Scheduler{ Id = 5, DateTime = new DateTime(2017,12,8,8,0,0), RoomId = 2, CourseId = 5},
                new Scheduler{ Id = 6, DateTime = new DateTime(2017,12,9,10,0,0), RoomId = 1, CourseId = 6},
                new Scheduler{ Id = 7, DateTime = new DateTime(2017,12,10,8,0,0), RoomId = 1, CourseId = 7},
                new Scheduler{ Id = 8, DateTime = new DateTime(2017,12,4,10,0,0), RoomId = 2, CourseId = 5},
                new Scheduler{ Id = 9, DateTime = new DateTime(2017,12,6,12,0,0), RoomId = 2, CourseId = 3},
                new Scheduler{ Id = 10, DateTime = new DateTime(2017,12,8,14,0,0), RoomId = 2, CourseId = 1},
            };
            context.Schedulers.AddOrUpdate(schedulers);
            context.SaveChanges();

            var subscriptions = new Subscription[]
            {
                new Subscription{Id = 1, PackageId = 1, StartDate = new DateTime(2017,12,8,14,0,0), Validity = 30},
                new Subscription{Id = 2, PackageId = 2, StartDate = new DateTime(2017,12,8,14,0,0), Validity = 30},
            };
            context.Subscriptions.AddOrUpdate(subscriptions);
            context.SaveChanges();

            var UserManager = new UserManager<User>(new UserStore<User>(context));
            List<string> ids = new List<string>();
            for (int i = 0; i <= 10; i++)
                ids.Add(Guid.NewGuid().ToString());
            var users = new User[]
            {
                new User{ Id = ids[0],Email = "test@yahoo.com", UserName = "test@yahoo.com", PasswordHash =  new PasswordHasher().HashPassword("Test1."),FirstName = "Maria", LastName = "Popescu", PhoneNumber = "0756321456", SubscriptionId = 1},
                new User{ Id = ids[1],Email = "alina@yahoo.com", UserName = "alina@yahoo.com", PasswordHash =  new PasswordHasher().HashPassword("Test1."),FirstName = "Alina", LastName = "Toader", PhoneNumber = "0756321456", SubscriptionId = 1},
                new User{ Id = ids[2],Email = "raluca@yahoo.com", UserName = "raluca@yahoo.com", PasswordHash =  new PasswordHasher().HashPassword("Test1."),FirstName = "Raluca", LastName = "Manea", PhoneNumber = "0756321456", SubscriptionId = 1},
                new User{ Id = ids[3],Email = "bogdan@yahoo.com", UserName = "bogdan@yahoo.com", PasswordHash =  new PasswordHasher().HashPassword("Test1."),FirstName = "Bogdan", LastName = "Vorobet", PhoneNumber = "0756321456", SubscriptionId = 1},
                new User{ Id = ids[4],Email = "andreiv@yahoo.com", UserName = "andreiv@yahoo.com", PasswordHash =  new PasswordHasher().HashPassword("Test1."),FirstName = "Andrei", LastName = "Vlad", PhoneNumber = "0756321456", SubscriptionId = 1},
                new User{ Id = ids[5],Email = "daniel@yahoo.com", UserName = "daniel@yahoo.com", PasswordHash =  new PasswordHasher().HashPassword("Test1."),FirstName = "Daniel", LastName = "Neamtu", PhoneNumber = "0756321456", SubscriptionId = 1},
                new User{ Id = ids[6],Email = "claudiu@yahoo.com", UserName = "claudiu@yahoo.com", PasswordHash =  new PasswordHasher().HashPassword("Test1."),FirstName = "Claudiu", LastName = "Ursuta", PhoneNumber = "0756321456", SubscriptionId = 1},
                new User{ Id = ids[7],Email = "andreic@yahoo.com", UserName = "andreic@yahoo.com", PasswordHash =  new PasswordHasher().HashPassword("Test1."),FirstName = "Andrei", LastName = "Caleniuc", PhoneNumber = "0756321456", SubscriptionId = 1},
            };
            foreach (var user in users)
                UserManager.Create(user);

            var usercourses = new UserCourse[]
            {
                new UserCourse{Id = 1, CourseId = 1, UserId = ids[0]},
                new UserCourse{Id = 2, CourseId = 2, UserId = ids[0]},
                new UserCourse{Id = 3, CourseId = 3, UserId = ids[0]},
                new UserCourse{Id = 4, CourseId = 4, UserId = ids[0]},
                new UserCourse{Id = 5, CourseId = 5, UserId = ids[0]},
                new UserCourse{Id = 6, CourseId = 6, UserId = ids[0]},
                new UserCourse{Id = 7, CourseId = 7, UserId = ids[0]},
                new UserCourse{Id = 8, CourseId = 8, UserId = ids[0]},
                new UserCourse{Id = 9, CourseId = 9, UserId = ids[0]},
                new UserCourse{Id = 10, CourseId = 10, UserId = ids[0]},
                new UserCourse{Id = 11, CourseId = 11, UserId = ids[0]},
                new UserCourse{Id = 12, CourseId = 12, UserId = ids[0]},
                new UserCourse{Id = 13, CourseId = 13, UserId = ids[0]},
                new UserCourse{Id = 14, CourseId = 14, UserId = ids[0]},
                new UserCourse{Id = 15, CourseId = 15, UserId = ids[0]},
                new UserCourse{Id = 16, CourseId = 16, UserId = ids[0]},
            };
            context.UserCourses.AddOrUpdate(usercourses);
            context.SaveChanges();

            var userschedulers = new UserScheduler[]
            {
                new UserScheduler{Id = 1, SchedulerId = 1, UserId = ids[0]},
                new UserScheduler{Id = 2, SchedulerId = 2, UserId = ids[0]},
                new UserScheduler{Id = 3, SchedulerId = 3, UserId = ids[0]},
                new UserScheduler{Id = 4, SchedulerId = 4, UserId = ids[0]},
                new UserScheduler{Id = 5, SchedulerId = 5, UserId = ids[0]},
                new UserScheduler{Id = 6, SchedulerId = 6, UserId = ids[0]},
                new UserScheduler{Id = 7, SchedulerId = 7, UserId = ids[0]},
                new UserScheduler{Id = 8, SchedulerId = 8, UserId = ids[0]},
                new UserScheduler{Id = 9, SchedulerId = 2, UserId = ids[0]},
            };
            context.UserSchedulers.AddOrUpdate(userschedulers);
            context.SaveChanges();
        }
    }
}
