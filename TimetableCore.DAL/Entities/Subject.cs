using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimetableCore.DAL.Entities
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        public string SubjectName { get; set; }
        public int IdProfessor { get; set; }
        [ForeignKey("IdProfessor")]
        public User User { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
        public ICollection<Exam> Exams { get; set; }
        public ICollection<Mark> Marks { get; set; }
    }
}
