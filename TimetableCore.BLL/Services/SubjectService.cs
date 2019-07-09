using System;
using System.Collections.Generic;
using System.Linq;
using TimetableCore.BLL.DTO;
using TimetableCore.BLL.Interfaces;
using TimetableCore.DAL.Entities;
using TimetableCore.DAL.Repositories;

namespace TimetableCore.BLL.Services
{
    public class SubjectService : ISubjectService
    {
        EFUnitOfWork Database { get; set; }

        public SubjectService(EFUnitOfWork uof)
        {
            Database = uof;
        }
        public void CreateSubject(SubjectDTO subjectDto)
        {
            if (subjectDto == null)
            {
                throw new Exception("Error. subject == null");
            }

            var professor = Database.Users.GetAll().FirstOrDefault(x => x.Login == subjectDto.Professor);


            Subject subject = new Subject
            {
                IdProfessor = professor.Id,
                SubjectName = subjectDto.SubjectName
            };

            Database.Subjects.Create(subject);
            Database.Save();
        }

        public void DeleteSubject(int id)
        {
            if (id == 0)
                throw new Exception("id == null");

            Database.Subjects.Delete(id);
            Database.Save();
        }

        public SubjectDTO GetSubject(int? id)
        {
            if (id == null)
                throw new Exception("id == null");

            var subject = Database.Subjects.Get(id.Value);

            if (subject == null)
                throw new Exception("subject don't find");

            return new SubjectDTO
            {
                Id = subject.Id,
                Professor = subject.User.Login,
                SubjectName = subject.SubjectName
            };
        }

        public IEnumerable<SubjectDTO> GetSubjects()
        {
            var subjects = Database.Subjects.GetAll();
            List<SubjectDTO> subjectdto = new List<SubjectDTO>();

            if (subjects == null)
            {
                throw new Exception("Error create list of subjects");
            }

            foreach (var s in subjects)
            {
                subjectdto.Add(new SubjectDTO
                {
                    Id = s.Id,
                    Professor = s.User.Login,
                    SubjectName = s.SubjectName
                });
            }

            return subjectdto;
        }

        public void UpdateSubject(SubjectDTO subjectDto)
        {
            if (subjectDto == null)
            {
                throw new Exception("error. update subject bll");
            }

            var professor = Database.Users.GetAll().FirstOrDefault(x => x.Login == subjectDto.Professor);

            Subject subject = new Subject
            {
                Id = subjectDto.Id,
                SubjectName=subjectDto.SubjectName,
                IdProfessor=professor.Id
            };          

            Database.Subjects.Update(subject);
            Database.Save();
        }

        public IEnumerable<SubjectDTO> SubjectsForProfessor(string loginprofessor)
        {
            var subjects = Database.Subjects.GetAll().Where(i=>i.User.Login==loginprofessor);

            List<SubjectDTO> subjectdto = new List<SubjectDTO>();

            if (subjects == null)
            {
                throw new Exception("Error create list of subjects");
            }

            foreach (var s in subjects)
            {
                subjectdto.Add(new SubjectDTO
                {
                    Id = s.Id,
                    Professor = s.User.Login,
                    SubjectName = s.SubjectName
                });
            }

            return subjectdto;          
        }
    }
}
