using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timetable.WEB.Models;
using TimetableCore.BLL.DTO;
using TimetableCore.BLL.Services;

namespace Timetable.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonAPIController : ControllerBase
    {
        LessonService lessonService;
        public LessonAPIController(LessonService serv)
        {
            lessonService = serv;
        }

        // GET: api/LessonAPI
        [Authorize(Roles = "admin, professor, student")]
        [HttpGet]
        public IActionResult GetLessons()
        {
            try
            {
                List<LessonViewModel> lessons = new List<LessonViewModel>();

                if (UserHelper.CurrentUserRole == "admin")
                {
                    var lessonsDto = lessonService.GetLessons();

                    if (lessonsDto == null)
                        throw new Exception("Error. lessons == null");

                    else
                    {
                        foreach (var l in lessonsDto)
                        {
                            lessons.Add(new LessonViewModel
                            {
                                Id = l.Id,
                                Auditorium = l.Auditorium,
                                Course = l.Course.ToString(),
                                Group = l.Group,
                                Subgroup = l.Subgroup.ToString(),
                                Subject = l.Subject,
                                Day = l.Day,
                                Time = l.Time
                            });
                        }
                        return Ok(lessons);
                    }
                }

                else if (UserHelper.CurrentUserRole == "professor")
                {
                    var lessonsDto = lessonService.GetLessonsForProfessor(UserHelper.CurrentUserId);

                    if (lessonsDto == null)
                        throw new Exception("Error. lessons == null");
                    else
                    {
                        foreach (var l in lessonsDto)
                        {
                            lessons.Add(new LessonViewModel
                            {
                                Id = l.Id,
                                Auditorium = l.Auditorium,
                                Course = l.Course.ToString(),
                                Group = l.Group,
                                Subgroup = l.Subgroup.ToString(),
                                Subject = l.Subject,
                                Day = l.Day,
                                Time = l.Time
                            });
                        }
                        return Ok(lessons);
                    }
                }

                else
                {
                    var lessonsDto = lessonService.GetLessonsForStudent(UserHelper.CurrentUserCourse.Value, UserHelper.CurrentUserSubgroup, UserHelper.CurrentUserGroup);

                    if (lessonsDto == null)
                        throw new Exception("Error. lessons == null");

                    else
                    {
                        foreach (var l in lessonsDto)
                        {
                            lessons.Add(new LessonViewModel
                            {
                                Id = l.Id,
                                Auditorium = l.Auditorium,
                                Course = l.Course.ToString(),
                                Group = l.Group,
                                Subgroup = l.Subgroup.ToString(),
                                Subject = l.Subject,
                                Day = l.Day,
                                Time = l.Time
                            });
                        }
                        return Ok(lessons);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/LessonAPI/5
        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public IActionResult GetLesson([FromRoute] int id)
        {
            try
            {
                var lessonDto = lessonService.GetLesson(id);

                if (lessonDto == null)
                    throw new Exception("Error. Lesson isn't found");

                else
                {
                    LessonViewModel lesson = new LessonViewModel
                    {
                        Id = lessonDto.Id,
                        Auditorium = lessonDto.Auditorium,
                        Course = lessonDto.Course.ToString(),
                        Group = lessonDto.Group,
                        Subgroup = lessonDto.Subgroup.ToString(),
                        Subject = lessonDto.Subject,
                        Day = lessonDto.Day,
                        Time = lessonDto.Time
                    };

                    return Ok(lesson);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/LessonAPI
        [Authorize(Roles = "admin, professor")]
        [HttpPut]
        public IActionResult PutLesson([FromBody] LessonViewModel lesson)
        {
            try
            {
                if (lesson == null)
                {
                    throw new Exception("Error. lesson == null");
                }

                if (lessonService.GetLesson(lesson.Id) == null)
                {
                    throw new Exception("Error. lesson doesn't found");
                }

                lessonService.UpdateLesson(new LessonDTO
                {
                    Id = lesson.Id,
                    Auditorium = lesson.Auditorium,
                    Course = Convert.ToInt32(lesson.Course),
                    Group = lesson.Group,
                    Subgroup = Convert.ToInt32(lesson.Subgroup),
                    Subject = lesson.Subject,
                    Day = lesson.Day,
                    Time = lesson.Time
                });

                return Ok(lesson);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/LessonAPI
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult PostLesson([FromForm] LessonViewModel lesson)
        {
            try
            {
                if (lesson == null)
                {
                    return BadRequest();
                }

                if (lesson.Id != 0)
                {
                    PutLesson(lesson);
                }

                else
                {
                    lessonService.CreateLesson(new LessonDTO
                    {
                        Id = lesson.Id,
                        Auditorium = lesson.Auditorium,
                        Course = Convert.ToInt32(lesson.Course),
                        Group = lesson.Group,
                        Subgroup = Convert.ToInt32(lesson.Subgroup),
                        Subject = lesson.Subject,
                        Day = lesson.Day,
                        Time = lesson.Time
                    });
                }

                return RedirectToAction("Lesson", "Admin");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/LessonAPI/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteLesson([FromRoute] int id)
        {
            try
            {
                if (lessonService.GetLesson(id) == null)
                {
                    throw new Exception("Error. lesson doesn't found");
                }
                else
                {
                    lessonService.DeleteLesson(id);
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}