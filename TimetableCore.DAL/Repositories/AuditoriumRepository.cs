using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TimetableCore.DAL.Entities;
using TimetableCore.DAL.Interfaces;

namespace TimetableCore.DAL.Repositories
{
    public class AuditoriumRepository : IRepository<Auditorium>
    {
        private ContextDB db;

        public AuditoriumRepository(ContextDB context)
        {
            db = context;
        }

        public IEnumerable<Auditorium> GetAll()
        {
            return db.Auditoriums;
        }

        public Auditorium Get(int id)
        {
            return db.Auditoriums.Find(id);
        }

        public void Create(Auditorium auditorium)
        {
            db.Auditoriums.Add(auditorium);
        }

        public void Update(Auditorium auditorium)
        {
            db.Auditoriums.Update(auditorium);
        }

        public IEnumerable<Auditorium> Find(Func<Auditorium, Boolean> predicate)
        {
            return db.Auditoriums.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Auditorium auditorium = db.Auditoriums.Find(id);
            if (auditorium != null)
                db.Auditoriums.Remove(auditorium);
        }
    }
}