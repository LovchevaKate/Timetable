using System;
using System.Collections.Generic;
using System.Linq;
using TimetableCore.BLL.DTO;
using TimetableCore.BLL.Interfaces;
using TimetableCore.DAL.Entities;
using TimetableCore.DAL.Repositories;

namespace TimetableCore.BLL.Services
{
    public class ExamService : IExamService
    {
        EFUnitOfWork Database { get; set; }

        public ExamService(EFUnitOfWork uof)
        {
            Database = uof;
        }
        public void CreateExam(ExamDTO examDto)
        {
            if (examDto == null)
            {
                throw new Exception("Error. exam == null");
            }

            var groupName = Database.Groups.GetAll().FirstOrDefault(x => x.GroupName == examDto.Group);
            var subjectName = Database.Subjects.GetAll().FirstOrDefault(x => x.SubjectName == examDto.Subject);
            var courseNumber = Database.Courses.GetAll().FirstOrDefault(x => x.CourseNumber == examDto.Course);

            Exam exam = new Exam
            {
                Date = examDto.Date,
                IdGroup = groupName.Id,
                IdSubject = subjectName.Id,
                IdCourse = courseNumber.Id
            };

            Database.Exams.Create(exam);
            Database.Save();
        }

        public void DeleteExam(int id)
        {
            if (id == 0)
                throw new Exception("id == null");

            Database.Exams.Delete(id);
            Database.Save();
        }

        public ExamDTO GetExam(int? id)
        {
            if (id == null)
                throw new Exception("id == null");

            var exam = Database.Exams.Get(id.Value);

            if (exam == null)
                throw new Exception("Exam don't find");

            return new ExamDTO
            {
                Id = exam.Id,
                Date = exam.Date,
                Group = exam.Groupp.GroupName,
                Subject = exam.Subject.SubjectName,
                Course = exam.Course.CourseNumber
            };
        }

        public IEnumerable<ExamDTO> GetExams()
        {
            var exams = Database.Exams.GetAll();
            List<ExamDTO> examsdto = new List<ExamDTO>();

            if (exams == null)
            {
                throw new Exception("Error create list of exams");
            }

            foreach (var e in exams)
            {
                ExamDTO examDto = new ExamDTO()
                {
                    Id = e.Id,
                    Date = e.Date
                };

                var group = Database.Groups.Get(e.IdGroup);
                examDto.Group = group.GroupName;
                var subject = Database.Subjects.Get(e.IdSubject);
                examDto.Subject = subject.SubjectName;
                var course = Database.Courses.Get(e.IdCourse);
                examDto.Course = course.CourseNumber;

                examsdto.Add(examDto);
            }

            return examsdto;
        }

        public IEnumerable<ExamDTO> GetExamsForProfessor(int idprofessor)
        {
            var exams = Database.Exams.GetAll().Where(l => l.Subject.IdProfessor == idprofessor);
            List<ExamDTO> examsdto = new List<ExamDTO>();

            if (exams == null)
            {
                throw new Exception("Error create list of exams");
            }

            foreach (var e in exams)
            {
                examsdto.Add(new ExamDTO
                {
                    Id = e.Id,
                    Date = e.Date,
                    Group = e.Groupp.GroupName,
                    Subject = e.Subject.SubjectName,
                    Course = e.Course.CourseNumber
                });
            }

            return examsdto;
        }

        public IEnumerable<ExamDTO> GetExamsForStudent(string group, int course)
        {
            var g = Database.Groups.GetAll().FirstOrDefault(x => x.GroupName == group);
            var c = Database.Courses.GetAll().FirstOrDefault(x => x.CourseNumber == course);

            var exams = Database.Exams.GetAll().Where(l => l.IdGroup == g.Id && l.IdCourse == c.Id);

            List<ExamDTO> examsdto = new List<ExamDTO>();

            if (exams == null)
            {
                throw new Exception("Error create list of exams");
            }

            foreach (var e in exams)
            {
                examsdto.Add(new ExamDTO
                {
                    Id = e.Id,
                    Date = e.Date,
                    Group = e.Groupp.GroupName,
                    Subject = e.Subject.SubjectName,
                    Course = e.Course.CourseNumber
                });
            }

            return examsdto;
        }

        public void UpdateExam(ExamDTO examDto)
        {
            if (examDto == null)
            {
                throw new Exception("error. update exam bll");
            }

            var groupName = Database.Groups.GetAll().FirstOrDefault(x => x.GroupName == examDto.Group);
            var subjectName = Database.Subjects.GetAll().FirstOrDefault(x => x.SubjectName == examDto.Subject);
            var courseNumber = Database.Courses.GetAll().FirstOrDefault(x => x.CourseNumber == examDto.Course);

            Exam exam = new Exam
            {
                Id = examDto.Id,
                Date = examDto.Date,
                IdSubject = subjectName.Id,
                IdGroup = groupName.Id,
                IdCourse = courseNumber.Id
            };

            Database.Exams.Update(exam);
            Database.Save();
        }
    }
}
