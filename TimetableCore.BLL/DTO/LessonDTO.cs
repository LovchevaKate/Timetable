using System;

namespace TimetableCore.BLL.DTO
{
    public class LessonDTO
    {
        public int Id { get; set; }
        public string Auditorium { get; set; }
        public string Subject { get; set; }
        public DayOfWeek Day { get; set; }
        public string Time { get; set; }
        public int Course { get; set; }
        public string Group { get; set; }
        public int? Subgroup { get; set; }
    }
}
