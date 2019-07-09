namespace Timetable.WEB.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FIO { get; set; }
        public string Role { get; set; }
        public string Group { get; set; }
        public int? Subgroup { get; set; }
        public int? Course { get; set; }
    }
}
