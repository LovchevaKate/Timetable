using System;
using System.Collections.Generic;
using System.Linq;
using TimetableCore.BLL.DTO;
using TimetableCore.BLL.Interfaces;
using TimetableCore.DAL.Entities;
using TimetableCore.DAL.Repositories;

namespace TimetableCore.BLL.Services
{
    public class LessonService : ILessonService
    {
        EFUnitOfWork Database { get; set; }

        public LessonService(EFUnitOfWork uof)
        {
            Database = uof;
        }
        public void CreateLesson(LessonDTO lessonDto)
        {
            Lesson lesson = new Lesson();

            var group = Database.Groups.GetAll().FirstOrDefault(x => x.GroupName == lessonDto.Group);
            var subgroup = Database.Subgroups.GetAll().FirstOrDefault(x => x.SubgroupNumber == lessonDto.Subgroup);
            var auditorium = Database.Auditoriums.GetAll().FirstOrDefault(x => x.AuditoriumName == lessonDto.Auditorium);
            var course = Database.Courses.GetAll().FirstOrDefault(x => x.CourseNumber == lessonDto.Course);
            var subject = Database.Subjects.GetAll().FirstOrDefault(x => x.SubjectName == lessonDto.Subject);
            var lessons = Database.Lessons.GetAll();

            if (lessonDto == null)
            {
                throw new Exception("Error. lesson == null");
            }

            if (lessonDto.Group != null)
            {
                lesson.IdGroup = group.Id;

                if (lessonDto.Subgroup != 0)
                {
                    lesson.IdSubgroup = subgroup.Id;
                }
            }


            foreach (var l in lessons)
            {
                if (l.Day == lessonDto.Day &&
                    l.Time == lessonDto.Time &&
                    l.Auditorium.AuditoriumName == auditorium.AuditoriumName)
                    throw new Exception("В это время, в этой аудитории уже есть пара");

                else if (l.Day == lessonDto.Day &&
                    l.Time == lessonDto.Time &&
                    (subgroup != null || group != null || l.Course.CourseNumber == course.CourseNumber))
                {
                        throw new Exception("В это время уже есть пара");
                }
            }

            lesson.Id = lessonDto.Id;
            lesson.IdAuditorium = auditorium.Id;
            lesson.IdCourse = course.Id;
            lesson.IdSubject = subject.Id;
            lesson.Day = lessonDto.Day;
            lesson.Time = lessonDto.Time;

            Database.Lessons.Create(lesson);
            Database.Save();
        }

        public void DeleteLesson(int id)
        {
            if (id == 0)
                throw new Exception("id == null");

            Database.Lessons.Delete(id);
            Database.Save();
        }

        public LessonDTO GetLesson(int? id)
        {
            if (id == null)
                throw new Exception("id == null");

            var lesson = Database.Lessons.Get(id.Value);

            if (lesson == null)
                throw new Exception("lesson don't find");

            LessonDTO lessonDto = new LessonDTO()
            {
                Id = lesson.Id,
                Auditorium = lesson.Auditorium.AuditoriumName,
                Course = lesson.Course.CourseNumber,
                Subject = lesson.Subject.SubjectName,
                Day = lesson.Day,
                Time = lesson.Time
            };

            if (lesson.Group == null)
                lessonDto.Group = null;
            else
                lessonDto.Group = lesson.Group.GroupName;

            if (lesson.Subgroup == null)
                lessonDto.Subgroup = null;
            else
                lessonDto.Subgroup = lesson.Subgroup.SubgroupNumber;

            return lessonDto;
        }

        public IEnumerable<LessonDTO> GetLessons()
        {
            var lessons = Database.Lessons.GetAll().OrderBy(o=>o.Time);
            List<LessonDTO> lessondto = new List<LessonDTO>();

            if (lessons == null)
            {
                throw new Exception("Error create list of lessons");
            }

            foreach (var l in lessons)
            {
                LessonDTO lessonDto = new LessonDTO
                {
                    Id = l.Id,
                    Auditorium = l.Auditorium.AuditoriumName,
                    Course = l.Course.CourseNumber,
                    Subject = l.Subject.SubjectName,
                    Day = l.Day,
                    Time = l.Time
                };

                if (l.Group == null)
                    lessonDto.Group = null;
                else
                    lessonDto.Group = l.Group.GroupName;

                if (l.Subgroup == null)
                    lessonDto.Subgroup = null;
                else
                    lessonDto.Subgroup = l.Subgroup.SubgroupNumber;

                lessondto.Add(lessonDto);
            }

            return lessondto;
        }

        public IEnumerable<LessonDTO> GetLessonsForProfessor(int idprofessor)
        {
            var lessons = Database.Lessons.GetAll().Where(i=>i.Subject.User.Id==idprofessor).OrderBy(o => o.Time);
            List<LessonDTO> lessondto = new List<LessonDTO>();

            if (lessons == null)
            {
                throw new Exception("Error create list of lessons");
            }

            foreach (var l in lessons)
            {
                LessonDTO lessonDto = new LessonDTO
                {
                    Id = l.Id,
                    Auditorium = l.Auditorium.AuditoriumName,
                    Course = l.Course.CourseNumber,
                    Subject = l.Subject.SubjectName,
                    Day = l.Day,
                    Time = l.Time
                };

                if (l.Group == null)
                    lessonDto.Group = null;
                else
                    lessonDto.Group = l.Group.GroupName;

                if (l.Subgroup == null)
                    lessonDto.Subgroup = null;
                else
                    lessonDto.Subgroup = l.Subgroup.SubgroupNumber;

                lessondto.Add(lessonDto);
            }

            return lessondto;
        }

        public IEnumerable<LessonDTO> GetLessonsForStudent(int course, int? subgroup, string group)
        {
            var g = Database.Groups.GetAll().FirstOrDefault(x => x.GroupName == group);
            var s = Database.Subgroups.GetAll().FirstOrDefault(x => x.SubgroupNumber == subgroup);

            var lessons = Database.Lessons.GetAll().Where(l => l.IdCourse == course || 
            (l.IdGroup == g.Id || l.IdGroup == 0) && 
            (l.IdSubgroup == s.Id || l.IdSubgroup == 0)).OrderBy(o => o.Time);

            List<LessonDTO> lessondto = new List<LessonDTO>();

            if (lessons == null)
            {
                throw new Exception("Error create list of lessons");
            }

            foreach (var l in lessons)
            {
                LessonDTO lessonDto = new LessonDTO
                {
                    Id = l.Id,
                    Auditorium = l.Auditorium.AuditoriumName,
                    Course = l.Course.CourseNumber,
                    Subject = l.Subject.SubjectName,
                    Day = l.Day,
                    Time = l.Time
                };

                if (l.Group == null)
                    lessonDto.Group = null;
                else
                    lessonDto.Group = l.Group.GroupName;

                if (l.Subgroup == null)
                    lessonDto.Subgroup = null;
                else
                    lessonDto.Subgroup = l.Subgroup.SubgroupNumber;

                lessondto.Add(lessonDto);
            }

            return lessondto;
        }

        public void UpdateLesson(LessonDTO lessonDto)
        {
            Lesson lesson = new Lesson();
            if (lessonDto == null)
            {
                throw new Exception("error. update lesson bll");
            }

            if (lessonDto.Group != null)
            {
                var groupName = Database.Groups.GetAll().FirstOrDefault(x => x.GroupName == lessonDto.Group);
                lesson.IdGroup = groupName.Id;

                if (lessonDto.Subgroup != 0)
                {
                    var subgroupNumber = Database.Subgroups.GetAll().FirstOrDefault(x => x.SubgroupNumber == lessonDto.Subgroup);
                    lesson.IdSubgroup = subgroupNumber.Id;
                }
            }

            var auditoriumName = Database.Auditoriums.GetAll().FirstOrDefault(x => x.AuditoriumName == lessonDto.Auditorium);
            var courseNumber = Database.Courses.GetAll().FirstOrDefault(x => x.CourseNumber == lessonDto.Course);
            var subjectName = Database.Subjects.GetAll().FirstOrDefault(x => x.SubjectName == lessonDto.Subject);
            
            lesson.Id = lessonDto.Id;
            lesson.IdAuditorium = auditoriumName.Id;
            lesson.IdCourse = courseNumber.Id;
            lesson.IdSubject = subjectName.Id;
            lesson.Day = lessonDto.Day;
            lesson.Time = lessonDto.Time;


            Database.Lessons.Update(lesson);
            Database.Save();
        }
    }
}
