using System.Collections.Generic;
using TimetableCore.BLL.DTO;

namespace TimetableCore.BLL.Interfaces
{
    public interface ILessonService
    {
        void CreateLesson(LessonDTO lesson);
        LessonDTO GetLesson(int? id);
        IEnumerable<LessonDTO> GetLessons();
        void UpdateLesson(LessonDTO lesson);
        void DeleteLesson(int id);
        IEnumerable<LessonDTO> GetLessonsForProfessor(int idprofessor);
        IEnumerable<LessonDTO> GetLessonsForStudent(int course, int? subgroup, string group);
    }
}
