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
    public class MarkAPIController : ControllerBase
    {
        MarkService markService;
        public MarkAPIController(MarkService serv)
        {
            markService = serv;
        }

        [Authorize(Roles = "professor, student")]
        [HttpGet]
        public IActionResult GetMarks([FromHeader]string Group, [FromHeader]int Course, [FromHeader]int Subgroup, [FromHeader]string Subject)
        {
            try
            {     
                List<MarkViewModel> marks = new List<MarkViewModel>();

                if (UserHelper.CurrentUserRole == "professor")
                {
                    if (Group == null || Course == 0 || Subgroup == 0 || Subject == null)
                    {
                        throw new Exception("Error. Write more information");
                    }

                    var marksDto = markService.GetMarksForProfessor(UserHelper.CurrentUserId, Course, Group, Subgroup, Subject);

                    if (marksDto == null)
                        throw new Exception("Error. marks == null");

                    else
                    {
                        foreach (var m in marksDto)
                        {

                            marks.Add(new MarkViewModel
                            {
                                Id = m.Id,
                                User = m.User,
                                Subject = m.Subject,
                                Value = m.Value
                            });
                        }
                        return Ok(marks);
                    }
                }
                else
                {
                    if (Subject == null)
                    {
                        throw new Exception("Error. Choose subject");
                    }

                    var marksDto = markService.GetMarksForStudent(UserHelper.CurrentUserCourse.Value,
                                                                  UserHelper.CurrentUserSubgroup,
                                                                  UserHelper.CurrentUserGroup,
                                                                  UserHelper.CurrentUserId, Subject);

                    if (marksDto == null)
                        throw new Exception("Error. marks == null");

                    else
                    {
                        foreach (var m in marksDto)
                        {
                            marks.Add(new MarkViewModel
                            {
                                Id = m.Id,
                                User = m.User,
                                Subject = m.Subject,
                                Value = m.Value
                            });
                        }

                        return Ok(marks);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "professor")]
        [HttpPut]
        public IActionResult PutMark([FromHeader]int Id, [FromHeader]string Subject, [FromHeader]string User, [FromHeader]int Mark)
        {
            try
            {
                if (Mark <= 0 && Mark >= 11)
                    throw new Exception("Error. Wrong mark");
                else
                {
                    MarkViewModel mark = new MarkViewModel
                    {
                        Id = Id,
                        Subject = Subject,
                        User = User,
                        Value = Mark
                    };

                    if (mark == null)
                    {
                        return BadRequest();
                    }

                    MarkDTO markDto = new MarkDTO
                    {
                        Id = mark.Id,
                        User = mark.User,
                        Subject = mark.Subject,
                        Value = mark.Value
                    };

                    markService.UpdateMark(markDto);

                    return RedirectToAction("ProfessorMark", "Professor");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}