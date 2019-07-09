using System;
using System.Collections.Generic;
using System.Linq;
using TimetableCore.DAL.Entities;
using TimetableCore.DAL.Interfaces;

namespace TimetableCore.DAL.Repositories
{
    public class CourseRepository : IRepository<Course>
    {
        private ContextDB db;

        public CourseRepository(ContextDB context)
        {
            db = context;
        }

        public IEnumerable<Course> GetAll()
        {
            return db.Courses;
        }

        public Course Get(int id)
        {
            return db.Courses.Find(id);
        }

        public void Create(Course course)
        {
            db.Courses.Add(course);
        }

        public void Update(Course course)
        {
            db.Courses.Update(course);
        }

        public IEnumerable<Course> Find(Func<Course, Boolean> predicate)
        {
            return db.Courses.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Course course = db.Courses.Find(id);
            if (course != null)
                db.Courses.Remove(course);
        }
    }
}