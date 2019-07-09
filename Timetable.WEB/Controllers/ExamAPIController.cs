using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Timetable.WEB.Models;
using TimetableCore.BLL.DTO;
using TimetableCore.BLL.Services;

namespace Timetable.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamAPIController : ControllerBase
    {
        ExamService examService;
        public ExamAPIController(ExamService serv)
        {
            examService = serv;
        }

        // GET: api/ExamAPI
        [Authorize(Roles = "admin, professor, student")]
        [HttpGet]
        public IActionResult GetExams()
        {
            try
            {
                List<ExamViewModel> exams = new List<ExamViewModel>();

                if (UserHelper.CurrentUserRole == "professor")
                {
                    var examsDto = examService.GetExamsForProfessor(UserHelper.CurrentUserId);

                    if (examsDto == null)
                        throw new Exception("Error. Exams don't found");
                    else
                    {
                        foreach (var e in examsDto)
                        {
                            exams.Add(new ExamViewModel
                            {
                                Id = e.Id,
                                Date = e.Date,
                                Group = e.Group,
                                Subject = e.Subject,
                                Course = e.Course
                            });
                        }
                    }

                    return Ok(exams);
                }


                else if (UserHelper.CurrentUserRole == "student")
                {
                    var examsDto = examService.GetExamsForStudent(UserHelper.CurrentUserGroup, UserHelper.CurrentUserCourse.Value);
                    if (examsDto == null)
                        throw new Exception("Error. Exams don't found");
                    else
                    {
                        foreach (var e in examsDto)
                        {
                            exams.Add(new ExamViewModel
                            {
                                Id = e.Id,
                                Date = e.Date,
                                Group = e.Group,
                                Subject = e.Subject,
                                Course = e.Course
                            });
                        }

                        return Ok(exams);
                    }
                }

                else
                {
                    var examsDto = examService.GetExams();
                    if (examsDto == null)
                        throw new Exception("Error. Exams don't found");
                    else
                    {
                        foreach (var e in examsDto)
                        {
                            exams.Add(new ExamViewModel
                            {
                                Id = e.Id,
                                Date = e.Date,
                                Group = e.Group,
                                Subject = e.Subject,
                                Course = e.Course
                            });
                        }
                    }

                    return Ok(exams);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/ExamAPI/5
        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public IActionResult GetExam([FromRoute] int id)
        {
            try
            {
                var examDto = examService.GetExam(id);

                ExamViewModel exam = new ExamViewModel
                {
                    Id = examDto.Id,
                    Date = examDto.Date,
                    Group = examDto.Group,
                    Subject = examDto.Subject,
                    Course = examDto.Course
                };

                if (exam == null)
                {
                    return NotFound();
                }

                return Ok(exam);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/ExamAPI
        [Authorize(Roles = "admin")]
        [HttpPut]
        public IActionResult PutExam(ExamViewModel exam)
        {
            try
            {
                if (exam == null)
                {
                    throw new Exception("Error. exam == null");
                }

                if (examService.GetExam(exam.Id) == null)
                {
                    throw new Exception("Error. exam doesn't found");
                }

                ExamDTO examDto = new ExamDTO
                {
                    Id = exam.Id,
                    Date = exam.Date,
                    Group = exam.Group,
                    Subject = exam.Subject,
                    Course = exam.Course
                };

                examService.UpdateExam(examDto);

                return RedirectToAction("Exam", "Admin");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/ExamAPI
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult PostExam([FromForm]ExamViewModel exam)
        {
            try
            {
                if (exam.Id != 0)
                {
                    PutExam(exam);
                }
                else
                {
                    ExamDTO examDto = new ExamDTO
                    {
                        Date = exam.Date,
                        Group = exam.Group,
                        Subject = exam.Subject,
                        Course = exam.Course
                    };
                    examService.CreateExam(examDto);
                }
                return RedirectToAction("Exam", "Admin");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/ExamAPI/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteExam([FromRoute] int id)
        {
            try
            {
                if (examService.GetExam(id) == null)
                {
                    throw new Exception("Error. exam doesn't found");
                }
                else
                {
                    examService.DeleteExam(id);

                    return RedirectToAction("Exam", "Admin");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}