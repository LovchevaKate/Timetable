using System.Collections.Generic;
using TimetableCore.BLL.DTO;

namespace TimetableCore.BLL.Interfaces
{
    public interface ICourseService
    {
        void CreateCourse(CourseDTO course);
        CourseDTO GetCourse(int? id);
        IEnumerable<CourseDTO> GetCourses();
        void UpdateCourse(CourseDTO course);
        void DeleteCourse(int id);
        //void Dispose();
    }
}
