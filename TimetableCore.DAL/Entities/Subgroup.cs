using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TimetableCore.DAL.Entities
{
    public class Subgroup
    {
        [Key]
        public int Id { get; set; }
        public int SubgroupNumber { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<Lesson> Lessons { get; set; }

    }
}
