using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TimetableCore.DAL.Entities
{
    public class Mark
    {
        [Key]
        public int Id { get; set; }
        public int Value { get; set; }
        public int IdUser { get; set; }
        [ForeignKey("IdUser")]
        public User User { get; set; }
        public int IdSubject { get; set; }
        [ForeignKey("IdSubject")]
        public Subject Subject { get; set; }
    }
}
