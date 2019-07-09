using System;

namespace TimetableCore.BLL.DTO
{
    public class ExamDTO
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Group { get; set; }
        public int Course { get; set; }
        public DateTime Date { get; set; }
    }
}
