using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TimetableCore.DAL.Entities;
using TimetableCore.DAL.Interfaces;

namespace TimetableCore.DAL.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private ContextDB db;

        public UserRepository(ContextDB context)
        {
            db = context;
        }

        public IEnumerable<User> GetAll()
        {
            var users = db.Users.Include(i => i.Role)
                .Include(i => i.Course)
                .Include(i => i.Group)
                .Include(i => i.Subgroup)
                .Include(x=>x.Marks)
                .AsNoTracking()
                .ToList();
            return users;
        }

        public User Get(int id)
        {
            return db.Users.Include(i => i.Role)
                .Include(i => i.Course)
                .Include(i => i.Group)
                .Include(i => i.Subgroup).AsNoTracking().FirstOrDefault(x=>x.Id==id);
        }

        public void Create(User user)
        {
            db.Users.Add(user);
        }

        public void Update(User user)
        {
            db.Users.Update(user);
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return db.Users.Include(o => o.Role)
                .Include(i => i.Course)
                .Include(i => i.Group)
                .Include(i => i.Subgroup)
                .Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            User user = db.Users.Find(id);
            if (user != null)
                db.Users.Remove(user);
        }

        public User Login (User user)
        {
            User u = db.Users
                    .Include(i => i.Role)
                    .Include(i => i.Course)
                    .Include(i => i.Group)
                    .Include(i => i.Subgroup)
                    .FirstOrDefault(i => i.Login == user.Login && i.Password == user.Password);
            return u;
        }
    }
}