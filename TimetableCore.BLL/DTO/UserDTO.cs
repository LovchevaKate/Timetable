namespace TimetableCore.BLL.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string RoleType { get; set; }
        public string FIO { get; set; }
        public string Group { get; set; }
        public int? Subgroup { get; set; }
        public int? Course { get; set; }
    }
}
