using System.Collections.Generic;
using TimetableCore.BLL.DTO;

namespace TimetableCore.BLL.Interfaces
{
    public interface IExamService
    {
        void CreateExam(ExamDTO exam);
        ExamDTO GetExam(int? id);
        IEnumerable<ExamDTO> GetExams();
        void UpdateExam(ExamDTO exam);
        void DeleteExam(int id);
        //void Dispose();
        IEnumerable<ExamDTO> GetExamsForStudent(string group, int course);
        IEnumerable<ExamDTO> GetExamsForProfessor(int idprofessor);
    }
}
