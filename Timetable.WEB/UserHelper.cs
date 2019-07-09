namespace Timetable.WEB
{
    public static class UserHelper
    {
        public static string CurrentUserRole { get; set; }
        public static string CurrentUserGroup { get; set; }
        public static int? CurrentUserSubgroup { get; set; }
        public static int? CurrentUserCourse { get; set; }
        public static string CurrentUserLogin { get; set; }
        public static int CurrentUserId { get; set; }

    }
}
