using System;
using System.Collections.Generic;
using System.Text;
using TimetableCore.DAL.Entities;

namespace TimetableCore.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        IRepository<Role> Roles { get; }
        IRepository<Auditorium> Auditoriums { get; }
        IRepository<Course> Courses { get; }
        IRepository<Exam> Exams { get; }
        IRepository<Groupp> Groups { get; }
        IRepository<Lesson> Lessons { get; }
        IRepository<Subgroup> Subgroups { get; }
        IRepository<Subject> Subjects { get; }
        IRepository<Mark> Marks { get; }

        void Save();
    }
}
