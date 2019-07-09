using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TimetableCore.DAL.Entities;
using TimetableCore.DAL.Interfaces;

namespace TimetableCore.DAL.Repositories
{
    public class SubjectRepository : IRepository<Subject>
    {
        private ContextDB db;

        public SubjectRepository(ContextDB context)
        {
            db = context;
        }

        public IEnumerable<Subject> GetAll()
        {
            return db.Subjects.Include(x=>x.User).AsNoTracking().ToList();
        }

        public Subject Get(int id)
        {
            return db.Subjects.Include(x=>x.User).AsNoTracking().FirstOrDefault(x=>x.Id==id);
        }

        public void Create(Subject subject)
        {
            db.Subjects.Add(subject);
        }

        public void Update(Subject subject)
        {
            db.Subjects.Update(subject);
        }

        public IEnumerable<Subject> Find(Func<Subject, bool> predicate)
        {
            return db.Subjects.Include(x=>x.User).Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Subject subject = db.Subjects.Find(id);
            if (subject != null)
                db.Subjects.Remove(subject);
        }
    }
}