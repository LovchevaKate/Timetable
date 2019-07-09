using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TimetableCore.DAL.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public string FIO { get; set; }
        public int IdRole { get; set; }
        [ForeignKey("IdRole")]
        public Role Role { get; set; }

        public int? IdGroup { get; set; }
        [ForeignKey("IdGroup")]
        public Groupp Group { get; set; }

        public int? IdSubgroup { get; set; }
        [ForeignKey("IdSubgroup")]
        public Subgroup Subgroup { get; set; }

        public int? IdCourse { get; set; }
        [ForeignKey("IdCourse")]
        public Course Course { get; set; }
        public ICollection<Subject> Subjects { get; set; }
        public ICollection<Mark> Marks { get; set; }

    }
}
