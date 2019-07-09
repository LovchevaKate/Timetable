using Microsoft.EntityFrameworkCore;
using TimetableCore.DAL.Entities;

namespace TimetableCore.DAL
{
    public class ContextDB : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Auditorium> Auditoriums { get; set; }
        public DbSet<Groupp> Groups { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Subgroup> Subgroups { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Mark> Marks { get; set; }

        public ContextDB(DbContextOptions<ContextDB> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(i => i.Role)
                .WithMany(i => i.Users)
                .HasForeignKey(i => i.IdRole)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(i => i.Course)
                .WithMany(i => i.Users)
                .HasForeignKey(i => i.IdCourse)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(i => i.Group)
                .WithMany(i => i.Users)
                .HasForeignKey(i => i.IdGroup)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(i => i.Subgroup)
                .WithMany(i => i.Users)
                .HasForeignKey(i => i.IdSubgroup)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Lesson>()
                .HasOne(i => i.Auditorium)
                .WithMany(i => i.Lessons)
                .HasForeignKey(i => i.IdAuditorium)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Lesson>()
                .HasOne(i => i.Subject)
                .WithMany(i => i.Lessons)
                .HasForeignKey(i => i.IdSubject)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Lesson>()
                .HasOne(i => i.Course)
                .WithMany(i => i.Lessons)
                .HasForeignKey(i => i.IdCourse)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Lesson>()
                .HasOne(i => i.Group)
                .WithMany(i => i.Lessons)
                .HasForeignKey(i => i.IdGroup)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Lesson>()
                .HasOne(i => i.Subgroup)
                .WithMany(i => i.Lessons)
                .HasForeignKey(i => i.IdSubgroup)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Exam>()
                .HasOne(i => i.Groupp)
                .WithMany(i => i.Exams)
                .HasForeignKey(i => i.IdGroup)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Exam>()
                .HasOne(i => i.Subject)
                .WithMany(i => i.Exams)
                .HasForeignKey(i => i.IdSubject)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Subject>()
                .HasOne(i => i.User)
                .WithMany(i => i.Subjects)
                .HasForeignKey(i => i.IdProfessor)
                .OnDelete(DeleteBehavior.Restrict);

            
            Role adminRole = new Role { Id = 1, Type = "admin" };
            Role studentRole = new Role { Id = 2, Type = "student" };
            Role professorRole = new Role { Id = 3, Type = "professor" };

            User adminUser = new User { Id = 1, Login = "admin", Password = "123", IdRole = adminRole.Id };

            Course course1 = new Course { Id = 1, CourseNumber = 1 };
            Course course2 = new Course { Id = 2, CourseNumber = 2 };
            Course course3 = new Course { Id = 3, CourseNumber = 3 };
            Course course4 = new Course { Id = 4, CourseNumber = 4 };

            Groupp groupp1 = new Groupp { Id = 1, GroupName = "ISIT-1" };
            Groupp groupp2 = new Groupp { Id = 2, GroupName = "ISIT-2" };
            Groupp groupp3 = new Groupp { Id = 3, GroupName = "ISIT-3" };
            Groupp groupp4 = new Groupp { Id = 4, GroupName = "POIT-4" };
            Groupp groupp5 = new Groupp { Id = 5, GroupName = "POIT-5" };
            Groupp groupp6 = new Groupp { Id = 6, GroupName = "POIT-6" };
            Groupp groupp7 = new Groupp { Id = 7, GroupName = "MOB-7" };
            Groupp groupp8 = new Groupp { Id = 8, GroupName = "MOB-8" };
            Groupp groupp9 = new Groupp { Id = 9, GroupName = "DEVI-9" };
            Groupp groupp10 = new Groupp { Id = 10, GroupName = "DEVI-10" };

            Subgroup subgroup1 = new Subgroup { Id = 1, SubgroupNumber = 1 };
            Subgroup subgroup2 = new Subgroup { Id = 2, SubgroupNumber = 2 };

            Auditorium a1 = new Auditorium { Id = 1, AuditoriumType = "lek", AuditoriumName = "101-1", AuditoriumCopasity = 15 };
            Auditorium a2 = new Auditorium { Id = 2, AuditoriumType = "lab", AuditoriumName = "102-1", AuditoriumCopasity = 30 };
            Auditorium a3 = new Auditorium { Id = 3, AuditoriumType = "pz", AuditoriumName = "103-1", AuditoriumCopasity = 100 };

            modelBuilder.Entity<Auditorium>().HasData(new Auditorium[] { a1, a2, a3 });
            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, studentRole, professorRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });
            modelBuilder.Entity<Course>().HasData(new Course[] { course1, course2, course3, course4 });
            modelBuilder.Entity<Groupp>().HasData(new Groupp[] { groupp1, groupp2, groupp3, groupp4, groupp5, groupp6, groupp7, groupp8, groupp9, groupp10 });
            modelBuilder.Entity<Subgroup>().HasData(new Subgroup[] { subgroup1, subgroup2 });

        }
    }
}