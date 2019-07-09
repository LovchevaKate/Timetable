using System;
using System.Collections.Generic;
using System.Text;
using TimetableCore.BLL.DTO;
using TimetableCore.BLL.Interfaces;
using TimetableCore.DAL.Entities;
using TimetableCore.DAL.Repositories;

namespace TimetableCore.BLL.Services
{
    public class RoleService : IRoleService
    {
        EFUnitOfWork Database { get; set; }

        public RoleService(EFUnitOfWork uof)
        {
            Database = uof;
        }
        public void CreateRole(RoleDTO roleDto)
        {
            if (roleDto == null)
            {
                throw new Exception("Error. role == null");
            }

            Role role = new Role
            {
                Type = roleDto.Type
            };

            Database.Roles.Create(role);
            Database.Save();
        }

        public void DeleteRole(int id)
        {
            if (id == 0)
                throw new Exception("id == null");

            Database.Roles.Delete(id);
            Database.Save();
        }

        //public void Dispose()
        //{
        //    Database.Dispose();
        //}

        public RoleDTO GetRole(int? id)
        {
            if (id == null)
                throw new Exception("id == null");

            var role = Database.Roles.Get(id.Value);

            if (role == null)
                throw new Exception("role don't find");

            return new RoleDTO
            {
                Id = role.Id,
                Type = role.Type
            };
        }

        public IEnumerable<RoleDTO> GetRoles()
        {
            var roles = Database.Roles.GetAll();
            List<RoleDTO> roledto = new List<RoleDTO>();

            if (roles == null)
            {
                throw new Exception("Error create list of roles");
            }

            foreach (var r in roles)
            {
                roledto.Add(new RoleDTO
                {
                    Id = r.Id,
                    Type = r.Type
                });
            }

            return roledto;
        }

        public void UpdateRole(RoleDTO roleDto)
        {
            if (roleDto == null)
            {
                throw new Exception("error. update role bll");
            }

            Role role = new Role
            {
                Id = roleDto.Id,
                Type = roleDto.Type
            };

            Database.Roles.Update(role);
            Database.Save();
        }
    }
}
