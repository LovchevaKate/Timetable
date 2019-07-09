using System.Collections.Generic;
using TimetableCore.BLL.DTO;
using TimetableCore.DAL.Entities;

namespace TimetableCore.BLL.Interfaces
{
    public interface IUserService
    {
        void CreateUser(UserDTO user);
        UserDTO GetUser(int? id);
        IEnumerable<UserDTO> GetUsers();
        void UpdateUser(UserDTO user);
        void DeleteUser(int id);
        IEnumerable<UserDTO> GetProfessors();
        UserDTO Login(UserDTO userDto);
        User GetUserDB(string login);
    }
}
