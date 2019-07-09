using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimetableCore.DAL.Entities
{
    public class Exam
    {
        [Key]
        public int Id { get; set; }
        public int IdSubject { get; set; }
        [ForeignKey("IdSubject")]
        public Subject Subject { get; set; }
        public int IdGroup { get; set; }
        [ForeignKey("IdGroup")]
        public Groupp Groupp { get; set; }
        public int IdCourse { get; set; }
        [ForeignKey("IdCourse")]
        public Course Course { get; set; }
        public DateTime Date { get; set; }
    }
}
