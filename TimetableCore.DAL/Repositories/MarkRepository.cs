using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimetableCore.DAL.Entities;
using TimetableCore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TimetableCore.DAL.Repositories
{
    public class MarkRepository : IRepository<Mark>
    {
        private ContextDB db;

        public MarkRepository(ContextDB context)
        {
            db = context;
        }

        public IEnumerable<Mark> GetAll()
        {
            return db.Marks.AsNoTracking().
                Include(x=> x.Subject).ThenInclude(x=>x.User).Include(x=>x.User).
                ToList();
        }

        public Mark Get(int id)
        {
            return db.Marks.AsNoTracking().Include(x => x.Subject).Include(x => x.User).FirstOrDefault(x => x.Id == id);
        }

        public void Create(Mark mark)
        {
            db.Marks.Add(mark);
        }

        public void Update(Mark mark)
        {
            db.Marks.Update(mark);
        }

        public IEnumerable<Mark> Find(Func<Mark, Boolean> predicate)
        {
            return db.Marks.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Mark mark = db.Marks.Find(id);
            if (mark != null)
                db.Marks.Remove(mark);
        }
    }
}