using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TimetableCore.DAL.Entities;
using TimetableCore.DAL.Interfaces;

namespace TimetableCore.DAL.Repositories
{
    public class ExamRepository : IRepository<Exam>
    {
        private ContextDB db;

        public ExamRepository(ContextDB context)
        {
            db = context;
        }

        public IEnumerable<Exam> GetAll()
        {
            return db.Exams.Include(x=>x.Course).Include(x=>x.Groupp).Include(x=>x.Subject).ToList();
        }

        public Exam Get(int id)
        {
            return db.Exams.Include(x=>x.Groupp).Include(x=>x.Subject).Include(x=>x.Course).FirstOrDefault(x=>x.Id==id);
        }

        public void Create(Exam exam)
        {
            db.Exams.Add(exam);
        }

        public void Update(Exam exam)
        {
            db.Exams.Update(exam);
        }

        public IEnumerable<Exam> Find(Func<Exam, Boolean> predicate)
        {
            return db.Exams.Include(i=>i.Groupp).Include(i=>i.Subject).Include(x=>x.Course).Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Exam exam = db.Exams.Find(id);
            if (exam != null)
                db.Exams.Remove(exam);
        }
    }
}