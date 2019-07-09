using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Timetable.WEB.Models;
using TimetableCore.BLL.Services;

namespace Timetable.WEB.Controllers
{
    public class ProfessorController : Controller
    {
        UserService userService;
        AuditoriumService auditoriumService;
        SubjectService subjectService;
        public ProfessorController(UserService serv, AuditoriumService aud, SubjectService sub)
        {
            userService = serv;
            auditoriumService = aud;
            subjectService = sub;
        }
        public IActionResult Index()
        {
            return View("IndexProfessor");
        }

        public IActionResult ProfessorLesson()
        {
            var auditoriumsDto = auditoriumService.GetAuditoriums();
            List<AuditoriumViewModel> auditoriumViewModel = new List<AuditoriumViewModel>();
            foreach (var a in auditoriumsDto)
            {
                auditoriumViewModel.Add(new AuditoriumViewModel
                {
                    Id = a.Id,
                    AuditoriumCopasity = a.AuditoriumCopasity,
                    AuditoriumName = a.AuditoriumName,
                    AuditoriumType = a.AuditoriumType
                });
            }
            SelectList audit = new SelectList(auditoriumViewModel, "Id", "AuditoriumName");
            ViewBag.Auditoriums = audit;


            var subjectsDto = subjectService.SubjectsForProfessor(UserHelper.CurrentUserLogin);
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
            SelectList subject = new SelectList(subjectViewModels, "Id", "SubjectName");
            ViewBag.Subjects = subject;

            return View("ProfessorLesson");
        }

        public IActionResult ProfessorExam()
        {
            var subjectsDto = subjectService.SubjectsForProfessor(UserHelper.CurrentUserLogin);
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
            SelectList subject = new SelectList(subjectViewModels, "Id", "SubjectName");
            ViewBag.Subjects = subject;

            return View("ProfessorExam");
        }

        public IActionResult ProfessorMark()
        {
            var subjectsDto = subjectService.SubjectsForProfessor(UserHelper.CurrentUserLogin);
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

            return View("ProfessorMark");
        }
    }
}