using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TimetableCore.DAL.Entities
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public int CourseNumber { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
        public ICollection<Exam> Exams { get; set; }


    }
}
