using System;
using System.Collections.Generic;
using System.Linq;
using TimetableCore.BLL.DTO;
using TimetableCore.BLL.Interfaces;
using TimetableCore.DAL.Entities;
using TimetableCore.DAL.Repositories;

namespace TimetableCore.BLL.Services
{
    public class UserService : IUserService
    {
        EFUnitOfWork Database { get; set; }

        public UserService(EFUnitOfWork uof)
        {
            Database = uof;
        }

        public void CreateUser(UserDTO userDto)
        {
            var users = Database.Users.GetAll();
            foreach(User u in users)
            {
                if (userDto.Login == u.Login)
                    throw new Exception("Error. Login is found");
            }
            User user = new User();
            if (userDto == null)
            {
                throw new Exception("Error. user == null");
            }

            var roletype = Database.Roles.GetAll().FirstOrDefault(x => x.Type == userDto.RoleType);            

            if (userDto.Course != null && userDto.Group != null && userDto.Subgroup != null)
            {
                var courseNumber = Database.Courses.GetAll().FirstOrDefault(x => x.CourseNumber == userDto.Course.Value);
                var groupName = Database.Groups.GetAll().FirstOrDefault(x => x.GroupName == userDto.Group);
                var subgroupNumber = Database.Subgroups.GetAll().FirstOrDefault(x => x.SubgroupNumber == userDto.Subgroup.Value);

                user.IdCourse = courseNumber.Id;
                user.IdGroup = groupName.Id;
                user.IdSubgroup = subgroupNumber.Id;
            }

            user.Login = userDto.Login;
            user.Password = userDto.Password;
            user.FIO = userDto.FIO;
            user.IdRole = roletype.Id;

            Database.Users.Create(user);
            Database.Save();
        }

        public UserDTO GetUser(int? id)
        {
            if (id == null)
                throw new Exception("id == null");

            var user = Database.Users.Get(id.Value);

            if (user == null)
                throw new Exception("User don't find");

            UserDTO userDto = new UserDTO();
            userDto.Id = user.Id;
            userDto.Login = user.Login;
            userDto.Password = user.Password;
            userDto.RoleType = user.Role.Type;
            if (user.Course != null)
                userDto.Course = user.Course.CourseNumber;
            else
                userDto.Course = 0;
            if (user.Group != null)
                userDto.Group = user.Group.GroupName;
            else
                userDto.Group = null;
            if (user.Subgroup != null)
                userDto.Subgroup = user.Subgroup.SubgroupNumber;
            else
                userDto.Subgroup = 0;

            return userDto;
        }

        public User GetUserDB(string login)
        {
            if (login == null)
                throw new Exception("login == null");

            var user = Database.Users.GetAll().Where(i => i.Login == login).FirstOrDefault();

            if (user == null)
                throw new Exception("User don't find");
            return user;
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            var users = Database.Users.GetAll();
            List<UserDTO> usersdto = new List<UserDTO>();

            if (users == null)
            {
                throw new Exception("Error create list of users");
            }

            foreach (var u in users)
            {
                UserDTO userDto = new UserDTO();
                userDto.Id = u.Id;
                userDto.Login = u.Login;
                userDto.Password = u.Password;
                userDto.RoleType = u.Role.Type;
                if (u.Course != null)
                    userDto.Course = u.Course.CourseNumber;
                else
                    userDto.Course = 0;
                if (u.Group != null)
                    userDto.Group = u.Group.GroupName;
                else
                    userDto.Group = null;
                if (u.Subgroup != null)
                    userDto.Subgroup = u.Subgroup.SubgroupNumber;
                else
                    userDto.Subgroup = 0;

                usersdto.Add(userDto);

            }

            return usersdto;
        }

        public void UpdateUser(UserDTO userDto)
        {
            if (userDto == null)
            {
                throw new Exception("error. update user bll");
            }

            var courseNumber = Database.Courses.GetAll().FirstOrDefault(x => x.CourseNumber == userDto.Course.Value);
            var groupName = Database.Groups.GetAll().FirstOrDefault(x => x.GroupName == userDto.Group);
            var roleType = Database.Roles.GetAll().FirstOrDefault(x => x.Type == userDto.RoleType);
            var subgroupNumber = Database.Subgroups.GetAll().FirstOrDefault(x => x.SubgroupNumber == userDto.Subgroup.Value);

            User user = new User
            {
                Id = userDto.Id,
                Login = userDto.Login,
                Password = userDto.Password,
                FIO = userDto.FIO,
                IdCourse = courseNumber.Id,
                IdGroup = groupName.Id,
                IdRole = roleType.Id,
                IdSubgroup = subgroupNumber.Id
            };          

            Database.Users.Update(user);
            Database.Save();
        }

        public void DeleteUser(int id)
        {
            if (id == 0)
                throw new Exception("id == null");

            Database.Users.Delete(id);
            Database.Save();
        }

        public UserDTO Login(UserDTO userDto)
        {
            var user = GetUsers().Where(i => i.Login == userDto.Login && i.Password == userDto.Password).FirstOrDefault();

            if (user != null)
                return user;
            else
                return user;
        }

        public IEnumerable<UserDTO> GetProfessors()
        {
            var professors = Database.Users.GetAll().Where(i => i.Role.Id == 3);
            List<UserDTO> professorsdto = new List<UserDTO>();

            if (professors == null)
            {
                throw new Exception("Error create list of professors");
            }

            foreach (var u in professors)
            {
                professorsdto.Add(new UserDTO { Login = u.Login, Password = u.Password, Id = u.Id, RoleType = u.Role.Type });
            }

            return professorsdto;
        }

    }
}
