using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimetableCore.DAL.Entities
{
    public class Lesson
    {
        [Key]
        public int Id { get; set; }
        public int IdAuditorium { get; set; }
        [ForeignKey("IdAuditorium")]
        public Auditorium Auditorium { get; set; }
        public int IdSubject { get; set; }
        [ForeignKey("IdSubject")]
        public Subject Subject { get; set; }
        public DayOfWeek Day { get; set; }
        public string Time { get; set; }
        public int IdCourse { get; set; }
        [ForeignKey("IdCourse")]
        public Course Course { get; set; }
        public int? IdGroup { get; set; }
        [ForeignKey("IdGroup")]
        public Groupp Group { get; set; }
        public int? IdSubgroup { get; set; }
        [ForeignKey("IdSubgroup")]
        public Subgroup Subgroup { get; set; }
    }
}
