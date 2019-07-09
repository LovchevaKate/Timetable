using System;

namespace Timetable.WEB.Models
{
    public class ExamViewModel
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Group { get; set; }
        public int Course { get; set; }
        public DateTime Date { get; set; }
    }
}
