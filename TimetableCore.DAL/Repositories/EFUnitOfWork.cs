using Microsoft.EntityFrameworkCore;
using System;
using TimetableCore.DAL.Entities;
using TimetableCore.DAL.Interfaces;

namespace TimetableCore.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private ContextDB db;
        private DbContextOptionsBuilder context;
        private UserRepository userRepository;
        private RoleRepository roleRepository;
        private AuditoriumRepository auditoriumRepository;
        private CourseRepository courseRepository;
        private ExamRepository examRepository;
        private GroupRepository groupRepository;
        private LessonRepository lessonRepository;
        private SubgroupRepository subgroupRepository;
        private SubjectRepository subjectRepository;
        private MarkRepository markRepository;

        public EFUnitOfWork(ContextDB connectionString)
        {
            db = connectionString;
        }
        public EFUnitOfWork(DbContextOptionsBuilder connectionString)
        {
            context = connectionString;
        }

        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public IRepository<Role> Roles
        {
            get
            {
                if (roleRepository == null)
                    roleRepository = new RoleRepository(db);
                return roleRepository;
            }
        }

        public IRepository<Auditorium> Auditoriums
        {
            get
            {
                if (auditoriumRepository == null)
                    auditoriumRepository = new AuditoriumRepository(db);
                return auditoriumRepository;
            }
        }

        public IRepository<Course> Courses
        {
            get
            {
                if (courseRepository == null)
                    courseRepository = new CourseRepository(db);
                return courseRepository;
            }
        }

        public IRepository<Exam> Exams
        {
            get
            {
                if (examRepository == null)
                    examRepository = new ExamRepository(db);
                return examRepository;
            }
        }

        public IRepository<Groupp> Groups
        {
            get
            {
                if (groupRepository == null)
                    groupRepository = new GroupRepository(db);
                return groupRepository;
            }
        }

        public IRepository<Lesson> Lessons
        {
            get
            {
                if (lessonRepository == null)
                    lessonRepository = new LessonRepository(db);
                return lessonRepository;
            }
        }

        public IRepository<Subgroup> Subgroups
        {
            get
            {
                if (subgroupRepository == null)
                    subgroupRepository = new SubgroupRepository(db);
                return subgroupRepository;
            }
        }

        public IRepository<Subject> Subjects
        {
            get
            {
                if (subjectRepository == null)
                    subjectRepository = new SubjectRepository(db);
                return subjectRepository;
            }
        }

        public IRepository<Mark> Marks
        {
            get
            {
                if (markRepository == null)
                    markRepository = new MarkRepository(db);
                return markRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        //private bool disposed = false;

        //public virtual void Dispose(bool disposing)
        //{
        //    if (!disposed)
        //    {
        //        if (disposing)
        //        {
        //            db.Dispose();
        //        }
        //        disposed = true;
        //    }
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
    }
}