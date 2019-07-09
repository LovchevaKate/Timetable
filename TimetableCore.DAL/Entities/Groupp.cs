using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TimetableCore.DAL.Entities
{
    public class Groupp
    {
        [Key]
        public int Id { get; set; }
        public string GroupName { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
        public ICollection<Exam> Exams { get; set; }
    }
}
