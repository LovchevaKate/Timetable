using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TimetableCore.DAL.Entities;
using TimetableCore.DAL.Interfaces;

namespace TimetableCore.DAL.Repositories
{
    public class SubgroupRepository : IRepository<Subgroup>
    {
        private ContextDB db;

        public SubgroupRepository(ContextDB context)
        {
            db = context;
        }

        public IEnumerable<Subgroup> GetAll()
        {
            return db.Subgroups.AsNoTracking().ToList();
        }

        public Subgroup Get(int id)
        {
            return db.Subgroups.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public void Create(Subgroup subgroup)
        {
            db.Subgroups.Add(subgroup);
        }

        public void Update(Subgroup subgroup)
        {
            db.Subgroups.Update(subgroup);
        }

        public IEnumerable<Subgroup> Find(Func<Subgroup, bool> predicate)
        {
            return db.Subgroups.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Subgroup subgroup = db.Subgroups.Find(id);
            if (subgroup != null)
                db.Subgroups.Remove(subgroup);
        }
    }
}