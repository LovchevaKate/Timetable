using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TimetableCore.DAL.Entities;
using TimetableCore.DAL.Interfaces;

namespace TimetableCore.DAL.Repositories
{
    public class RoleRepository : IRepository<Role>
    {
        private ContextDB db;

        public RoleRepository(ContextDB context)
        {
            db = context;
        }

        public IEnumerable<Role> GetAll()
        {
            return db.Roles.AsNoTracking().ToList();
        }

        public Role Get(int id)
        {
            return db.Roles.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public void Create(Role role)
        {
            db.Roles.Add(role);
        }

        public void Update(Role role)
        {
            db.Roles.Update(role);
        }

        public IEnumerable<Role> Find(Func<Role, bool> predicate)
        {
            return db.Roles.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Role role = db.Roles.Find(id);
            if (role != null)
                db.Roles.Remove(role);
        }
    }
}