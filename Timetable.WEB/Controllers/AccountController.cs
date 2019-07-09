using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Timetable.WEB.Models;
using TimetableCore.BLL.DTO;
using TimetableCore.BLL.Services;
using TimetableCore.DAL.Entities;

namespace Timetable.WEB.Controllers
{
    public class AccountController : Controller
    {
        UserService userService;
        public AccountController(UserService serv)
        {
            userService = serv;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserDTO userDto = new UserDTO
                    {
                        Id = model.Id,
                        Login = model.Login,
                        Password = model.Password,
                        FIO = model.FIO,
                        Course = model.Course,
                        Group = model.Group,
                        RoleType = model.Role,
                        Subgroup = model.Subgroup
                    };

                    UserDTO u = userService.Login(userDto);

                    if (u != null)
                    {
                        await Authenticate(u); // аутентификация

                        UserHelper.CurrentUserRole = u.RoleType;
                        UserHelper.CurrentUserId = u.Id;
                        UserHelper.CurrentUserCourse = u.Course;
                        UserHelper.CurrentUserGroup = u.Group;
                        UserHelper.CurrentUserSubgroup = u.Subgroup;
                        UserHelper.CurrentUserLogin = u.Login;

                        switch (UserHelper.CurrentUserRole)
                        {
                            case "admin": return RedirectToAction("IndexAdmin", "Home");
                            case "student": return RedirectToAction("IndexStudent", "Home");
                            case "professor": return RedirectToAction("IndexProfessor", "Home");
                        }
                    }
                    else throw new Exception("Login or password isn't correct"); 
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        private async Task Authenticate(UserDTO userV)
        {
            User user = userService.GetUserDB(userV.Login);

            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Type)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            UserHelper.CurrentUserRole = null;
            UserHelper.CurrentUserId = 0;
            UserHelper.CurrentUserCourse = 0;
            UserHelper.CurrentUserGroup = null;
            UserHelper.CurrentUserSubgroup = 0;

            return RedirectToAction("Login", "Account");
        }


    }
}