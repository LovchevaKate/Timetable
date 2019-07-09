using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TimetableCore.DAL.Entities;
using TimetableCore.DAL.Interfaces;

namespace TimetableCore.DAL.Repositories
{
    public class LessonRepository : IRepository<Lesson>
    {
        private ContextDB db;

        public LessonRepository(ContextDB context)
        {
            db = context;
        }

        public IEnumerable<Lesson> GetAll()
        {
            return db.Lessons.Include(x => x.Auditorium)
                .Include(x => x.Course)
                .Include(x => x.Group)
                .Include(x => x.Subgroup)
                .Include(x => x.Subject)
                .ThenInclude(x=>x.User).AsNoTracking().ToList();
        }

        public Lesson Get(int id)
        {
            return db.Lessons.Include(x=>x.Auditorium)
                .Include(x=>x.Course)
                .Include(x=>x.Group)
                .Include(x=>x.Subgroup)
                .Include(x=>x.Subject).AsNoTracking().FirstOrDefault(x=>x.Id==id);
        }

        public void Create(Lesson lesson)
        {
            db.Lessons.Add(lesson);
        }

        public void Update(Lesson lesson)
        {
            db.Lessons.Update(lesson);
        }

        public IEnumerable<Lesson> Find(Func<Lesson, Boolean> predicate)
        {
            return db.Lessons.Include(x => x.Auditorium)
                .Include(x => x.Course)
                .Include(x => x.Group)
                .Include(x => x.Subgroup)
                .Include(x => x.Subject)
                .Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Lesson lesson = db.Lessons.Find(id);
            if (lesson != null)
                db.Lessons.Remove(lesson);
        }
    }
}