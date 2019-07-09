using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TimetableCore.DAL.Entities
{
    public class Auditorium
    {
        [Key]
        public int Id { get; set; }
        public string AuditoriumName { get; set; }
        public int AuditoriumCopasity { get; set; }
        public string AuditoriumType { get; set; }
        public ICollection<Lesson> Lessons { get; set; }

    }
}
