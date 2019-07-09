using System;
using System.Collections.Generic;
using System.Linq;
using TimetableCore.BLL.DTO;
using TimetableCore.BLL.Interfaces;
using TimetableCore.DAL.Entities;
using TimetableCore.DAL.Repositories;

namespace TimetableCore.BLL.Services
{
    public class MarkService : IMarkService
    {
        EFUnitOfWork Database { get; set; }

        public MarkService(EFUnitOfWork uof)
        {
            Database = uof;
        }
        public void CreateMark(MarkDTO markDto)
        {
            if (markDto == null)
            {
                throw new Exception("Error. mark == null");
            }

            var subject = Database.Subjects.GetAll().FirstOrDefault(x => x.SubjectName == markDto.Subject);
            var user = Database.Users.GetAll().FirstOrDefault(x => x.Login == markDto.User);

            Mark mark = new Mark
            {
                IdSubject = subject.Id,
                IdUser = user.Id,
                Value = markDto.Value
            };

            Database.Marks.Create(mark);
            Database.Save();
        }

        public IEnumerable<MarkDTO> GetMarksForProfessor(int currentUserId, int course, string group, int subgroup, string subject)
        {
            //var marks = Database.Marks.GetAll().Where(x => x.Subject.User.Id == currentUserId && x.Subject.SubjectName==subject
            //&&(x.User.IdCourse == course || (x.User.Group.GroupName == group || x.User.Group.GroupName == null) &&
            //(x.User.Subgroup.SubgroupNumber == subgroup || x.User.Subgroup.SubgroupNumber == 0)));

            var c = Database.Courses.GetAll().FirstOrDefault(x => x.CourseNumber == course);
            var g = Database.Groups.GetAll().FirstOrDefault(x => x.GroupName == group);
            var s = Database.Subgroups.GetAll().FirstOrDefault(x => x.SubgroupNumber == subgroup);
            var users = Database.Users.GetAll().Where(x => x.IdCourse == c.Id && x.IdGroup == g.Id && x.IdSubgroup == s.Id).ToList();

            var marks = Database.Marks.GetAll().Where(x => x.Subject.User.Id == currentUserId && x.Subject.SubjectName == subject
            && x.User.IdCourse == c.Id && x.User.IdGroup == g.Id && x.User.IdSubgroup == s.Id).ToList();

            List<MarkDTO> marksdto = new List<MarkDTO>();

                foreach(User u in users)
                {
                    if(u.Marks.Count == 0)
                    {
                        MarkDTO mark = new MarkDTO
                        {
                            User = u.Login,
                            Subject = subject,
                            Value = 0
                        };
                        CreateMark(mark);
                    }
                    
                }
            

            if (marks == null)
            {
                throw new Exception("Error create list of marks");
            }

            var marksResult = Database.Marks.GetAll().Where(x => x.Subject.User.Id == currentUserId && x.Subject.SubjectName == subject
            && x.User.IdCourse == c.Id && x.User.IdGroup == g.Id && x.User.IdSubgroup == s.Id).ToList();

            foreach (var m in marksResult)
            {
                MarkDTO markDto = new MarkDTO
                {
                    Id = m.Id,
                    User = m.User.Login,
                    Subject = m.Subject.SubjectName,
                    Value = m.Value
                };

                marksdto.Add(markDto);
            }

            return marksdto;
        }

        public IEnumerable<MarkDTO> GetMarksForStudent(int course, int? subgroup, string group, int user, string subject)
        {
            var g = Database.Groups.GetAll().FirstOrDefault(x => x.GroupName == group);
            var s = Database.Subgroups.GetAll().FirstOrDefault(x => x.SubgroupNumber == subgroup);

            var marks = Database.Marks.GetAll().Where(l => (l.User.IdCourse == course ||
            (l.User.IdGroup == g.Id || l.User.IdGroup == 0) &&
            (l.User.IdSubgroup == s.Id || l.User.IdSubgroup == 0)) && l.User.Id == user && l.Subject.SubjectName==subject);
            List<MarkDTO> marksdto = new List<MarkDTO>();

            if (marks == null)
            {
                throw new Exception("Error create list of marks");
            }

            foreach (var m in marks)
            {
                MarkDTO markDto = new MarkDTO
                {
                    Id = m.Id,
                    User = m.User.Login,
                    Subject = m.Subject.SubjectName,
                    Value = m.Value
                };

                marksdto.Add(markDto);
            }

            return marksdto;
        }

        public void DeleteMark(int id)
        {
            if (id == 0)
                throw new Exception("id == null");

            Database.Marks.Delete(id);
            Database.Save();
        }

        public MarkDTO GetMark(int? id)
        {
            if (id == null)
                throw new Exception("id == null");

            var mark = Database.Marks.Get(id.Value);

            if (mark == null)
                throw new Exception("mark don't find");

            MarkDTO markDto = new MarkDTO()
            {
                Id = mark.Id,
                User = mark.User.Login,
                Subject = mark.Subject.SubjectName,
                Value = mark.Value
            };

            return markDto;
        }

        public IEnumerable<MarkDTO> GetMarks()
        {
            var marks = Database.Marks.GetAll();
            List<MarkDTO> marksdto = new List<MarkDTO>();

            if (marks == null)
            {
                throw new Exception("Error create list of marks");
            }

            foreach (var m in marks)
            {
                MarkDTO markDto = new MarkDTO
                {
                    Id = m.Id,
                    User = m.User.Login,
                    Subject = m.Subject.SubjectName,
                    Value = m.Value
                };

                marksdto.Add(markDto);
            }

            return marksdto;
        }

        public void UpdateMark(MarkDTO markDto)
        {
            if (markDto == null)
            {
                throw new Exception("error. update mark bll");
            }

            var user = Database.Users.GetAll().FirstOrDefault(x => x.Login == markDto.User);
            var subject = Database.Subjects.GetAll().FirstOrDefault(x => x.SubjectName == markDto.Subject);

            Mark mark = new Mark
            {
                Id = markDto.Id,
                IdUser = user.Id,
                IdSubject = subject.Id,
                Value = markDto.Value
            };

            Database.Marks.Update(mark);
            Database.Save();
        }
    }
}
