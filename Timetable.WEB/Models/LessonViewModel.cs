using System;

namespace Timetable.WEB.Models
{
    public class LessonViewModel
    {
        public int Id { get; set; }
        public string Auditorium { get; set; }
        public string Subject { get; set; }
        public DayOfWeek Day { get; set; }
        public string Time { get; set; }
        public string Course { get; set; }
        public string Group { get; set; }
        public string Subgroup { get; set; }
    }
}
