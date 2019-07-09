using System.Collections.Generic;
using TimetableCore.BLL.DTO;

namespace TimetableCore.BLL.Interfaces
{
    public interface IMarkService
    {
        void CreateMark(MarkDTO mark);
        MarkDTO GetMark(int? id);
        IEnumerable<MarkDTO> GetMarks();
        void UpdateMark(MarkDTO mark);
        void DeleteMark(int id);
        IEnumerable<MarkDTO> GetMarksForProfessor(int currentUserId, int course, string group, int subgroup, string subject);
        IEnumerable<MarkDTO> GetMarksForStudent(int course, int? subgroup, string group, int user, string subject);
    }
}
