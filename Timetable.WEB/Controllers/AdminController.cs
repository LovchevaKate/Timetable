using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TimetableCore.BLL.Services;

namespace Timetable.WEB.Controllers
{
    public class AdminController : Controller
    {
        UserService userService;
        AuditoriumService auditoriumService;
        SubjectService subjectService;
        public AdminController(UserService serv, AuditoriumService aud, SubjectService sub)
        {
            userService = serv;
            auditoriumService = aud;
            subjectService = sub;
        }

        [Authorize(Roles = "admin, student, professor")]
        public IActionResult Index()
        {
            return View("IndexAdmin");
        }

        public IActionResult User()
        {
            return View("Users");
        }

        public IActionResult Subject()
        {
            SelectList user = new SelectList(userService.GetProfessors(),"Login", "Login");
            ViewBag.User = user;

            return View("Subject");
        }

        public IActionResult Lesson()
        {
            SelectList audit = new SelectList(auditoriumService.GetAuditoriums(), "AuditoriumName", "AuditoriumName");
            ViewBag.Auditoriums = audit;

            SelectList subject = new SelectList(subjectService.GetSubjects(), "SubjectName", "SubjectName");
            ViewBag.Subjects = subject;

            return View("Lesson");
        }

        public IActionResult Exam()
        {
            SelectList subject = new SelectList(subjectService.GetSubjects(), "SubjectName", "SubjectName");
            ViewBag.Subjects = subject;

            return View("Exam");
        }
    }
}