using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timetable.WEB.Models;
using TimetableCore.BLL.DTO;
using TimetableCore.BLL.Services;

namespace Timetable.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectAPIController : ControllerBase
    {
        SubjectService subjectService;
        public SubjectAPIController(SubjectService sub)
        {
            subjectService = sub;
        }

        // GET: api/SubjectAPI
        [Authorize(Roles = "admin, student, professor")]
        [HttpGet]
        public async Task<IEnumerable<SubjectViewModel>> GetSubjects()
        {
            var subjectDto = subjectService.GetSubjects();

            List<SubjectViewModel> subjectViewModel = new List<SubjectViewModel>();
            foreach (var s in subjectDto)
            {
                subjectViewModel.Add(new SubjectViewModel
                {
                    Id = s.Id,
                    Professor = s.Professor,
                    SubjectName = s.SubjectName
                });
            }
            return subjectViewModel;
        }

        // GET: api/SubjectAPI/5
        [Authorize(Roles = "admin, professor, student")]
        [HttpGet("{id}")]
        public IActionResult GetSubject([FromRoute] int id)
        {
            var subjectDto = subjectService.GetSubject(id);

            SubjectViewModel subject = new SubjectViewModel
            {
                Id = subjectDto.Id,
                Professor = subjectDto.Professor,
                SubjectName = subjectDto.SubjectName
            };

            return Ok(subject);
        }

        // PUT: api/SubjectAPI
        [Authorize(Roles = "admin")]
        [HttpPut]
        public IActionResult PutSubject([FromBody] SubjectViewModel subject)
        {
            SubjectDTO subjectDto = new SubjectDTO
            {
                Id = subject.Id,
                Professor = subject.Professor,
                SubjectName = subject.SubjectName
            };
            subjectService.UpdateSubject(subjectDto);

            return Ok(subject);
        }

        // POST: api/SubjectAPI
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult PostSubject([FromForm] SubjectViewModel subject)
        {
            if (subject.Id != 0)
            {
                PutSubject(subject);
            }

            else
            {
                SubjectDTO subjectDto = new SubjectDTO
                {
                    Id = subject.Id,
                    Professor = subject.Professor,
                    SubjectName = subject.SubjectName
                };

                subjectService.CreateSubject(subjectDto);
            }

            return RedirectToAction("Subject", "Admin");
        }

        // DELETE: api/SubjectAPI/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject([FromRoute] int id)
        {
            subjectService.DeleteSubject(id);

            return Ok();
        }
    }
}