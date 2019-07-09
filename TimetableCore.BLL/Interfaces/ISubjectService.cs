using System.Collections.Generic;
using TimetableCore.BLL.DTO;

namespace TimetableCore.BLL.Interfaces
{
    public interface ISubjectService
    {
        void CreateSubject(SubjectDTO subject);
        SubjectDTO GetSubject(int? id);
        IEnumerable<SubjectDTO> GetSubjects();
        void UpdateSubject(SubjectDTO subject);
        void DeleteSubject(int id);
        IEnumerable<SubjectDTO> SubjectsForProfessor(string loginprofessor);
    }
}
