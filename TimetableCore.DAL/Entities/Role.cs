using System.Collections.Generic;

namespace TimetableCore.DAL.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
