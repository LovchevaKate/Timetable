using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Timetable.WEB.Models;
using TimetableCore.BLL.Services;

namespace Timetable.WEB.Controllers
{
    public class StudentController : Controller
    {
        UserService userService;
        ExamService examService;
        SubjectService subjectService;
        public StudentController(UserService serv, ExamService exam, SubjectService subj)
        {
            userService = serv;
            examService = exam;
            subjectService = subj;
        }

        public IActionResult Index()
        {
            return View("IndexStudent");
        }

        public IActionResult StudentExam()
        {
            SelectList exam = new SelectList(examService.GetExams(), "Subject", "Subject");
            ViewBag.Exam = exam;

            return View("StudentExam");
        }

        public IActionResult StudentMark()
        {
            var subjectsDto = subjectService.GetSubjects();
            List<SubjectViewModel> subjectViewModels = new List<SubjectViewModel>();
            foreach (var s in subjectsDto)
            {
                subjectViewModels.Add(new SubjectViewModel
                {
                    Id = s.Id,
                    Professor = s.Professor,
                    SubjectName = s.SubjectName
                });
            }
            SelectList subject = new SelectList(subjectViewModels, "SubjectName", "SubjectName");
            ViewBag.Subjects = subject;

            return View("StudentMark");
        }
    }
}