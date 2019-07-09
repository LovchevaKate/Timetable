using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TimetableCore.DAL.Entities;
using TimetableCore.DAL.Interfaces;

namespace TimetableCore.DAL.Repositories
{
    public class GroupRepository : IRepository<Groupp>
    {
        private ContextDB db;

        public GroupRepository(ContextDB context)
        {
            db = context;
        }

        public IEnumerable<Groupp> GetAll()
        {
            return db.Groups.AsNoTracking().ToList();
        }

        public Groupp Get(int id)
        {
            return db.Groups.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public void Create(Groupp group)
        {
            db.Groups.Add(group);
        }

        public void Update(Groupp group)
        {
            db.Groups.Update(group);
        }

        public IEnumerable<Groupp> Find(Func<Groupp, Boolean> predicate)
        {
            return db.Groups.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Groupp group = db.Groups.Find(id);
            if (group != null)
                db.Groups.Remove(group);
        }
    }
}