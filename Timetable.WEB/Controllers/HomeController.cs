using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using TimetableCore.BLL.Services;

namespace Timetable.WEB.Controllers
{
    public class HomeController : Controller
    {
        UserService userService;
        public HomeController(UserService serv)
        {
            userService = serv;
        }
        public IActionResult Index()
        {            
            if(UserHelper.CurrentUserRole==null)
            {
                return RedirectToAction("Login", "Account");
            }
            switch (UserHelper.CurrentUserRole)
            {
                case "admin": return IndexAdmin();
                case "student": return IndexStudent();
                case "professor": return IndexProfessor();
                default:return View();
            }
        }

        [Authorize(Roles = "admin")]
        public IActionResult IndexAdmin()
        {
            return RedirectToAction("Index", "Admin");
        }

        [Authorize(Roles = "student")]
        public IActionResult IndexStudent()
        {
            return RedirectToAction("Index", "Student");
        }

        [Authorize(Roles = "professor")]
        public IActionResult IndexProfessor()
        {
            return RedirectToAction("Index", "Professor");
        }

        public IActionResult Test()
        {
            return View("Index");
        }
    }
}
