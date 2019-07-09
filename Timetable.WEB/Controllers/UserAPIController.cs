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
    public class UserAPIController : ControllerBase
    {
        readonly UserService userService;
        public UserAPIController(UserService serv)
        {
            userService = serv;
        }

        // GET: api/UsersAPI
        [Authorize(Roles = "admin")]
        [HttpGet]
        //public IEnumerable<UserViewModel> GetUsers()
        public IActionResult GetUsers()
        {
            try
            {
                List<UserViewModel> user = new List<UserViewModel>();
                var userDto = userService.GetUsers();

                if (userDto == null)
                {
                    throw new Exception("users don't found");
                }

                else
                {
                    foreach (var u in userDto)
                    {
                        user.Add(new UserViewModel
                        {
                            Id = u.Id,
                            Login = u.Login,
                            Password = u.Password,
                            FIO = u.FIO,
                            Course = u.Course,
                            Group = u.Group,
                            Role = u.RoleType,
                            Subgroup = u.Subgroup
                        });
                    }
                }

                return Ok(user);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/UsersAPI/5
        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public IActionResult GetUser([FromRoute] int id)
        {
            try
            {
                var user = userService.GetUser(id);

                if (user == null)
                {
                    throw new Exception("user doesn't found");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/UsersAPI
        [Authorize(Roles = "admin")]
        [HttpPut]
        public IActionResult PutUser(UserViewModel user)
        {
            try
            {
                if (user == null)
                {
                    throw new Exception("user==null");
                }

                if (userService.GetUser(user.Id) == null)
                {
                    throw new Exception("user doesn't found");
                }

                UserDTO userDto = new UserDTO
                {
                    Id = user.Id,
                    Login = user.Login,
                    Password = user.Password,
                    FIO = user.FIO,
                    Course = user.Course,
                    Group = user.Group,
                    RoleType = user.Role,
                    Subgroup = user.Subgroup
                };

                userService.UpdateUser(userDto);

                return RedirectToAction("User", "Admin");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/UsersAPI
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult PostUser([FromForm]UserViewModel user)
        {
            try
            {
                if (user.Login == null || user.Password == null)
                {
                    throw new Exception("Error. Password == null or Login == null");
                }
                if (user.Role == "student")
                {
                    if (user.Course == null || user.Group == null || user.Subgroup == null)
                        throw new Exception("Error. Info about student == null");
                }
                UserDTO userDto = new UserDTO
                {
                    Id = user.Id,
                    Login = user.Login,
                    Password = user.Password,
                    FIO = user.FIO,
                    Course = user.Course,
                    Group = user.Group,
                    RoleType = user.Role,
                    Subgroup = user.Subgroup
                };

                if (user.Id != 0)
                {
                    PutUser(user);
                }

                else
                {
                    userService.CreateUser(userDto);
                }

                return RedirectToAction("User", "Admin");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/UsersAPI/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteUser([FromRoute] int id)
        {
            try
            {
                if (userService.GetUser(id) == null)
                {
                    throw new Exception("Error. user doesn't found");
                }
                else
                {
                    userService.DeleteUser(id);

                    return RedirectToAction("User", "Admin");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}