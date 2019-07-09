using System;
using System.Collections.Generic;
using TimetableCore.BLL.DTO;
using TimetableCore.BLL.Interfaces;
using TimetableCore.DAL.Entities;
using TimetableCore.DAL.Repositories;

namespace TimetableCore.BLL.Services
{
    public class GroupService : IGroupService
    {
        EFUnitOfWork Database { get; set; }

        public GroupService(EFUnitOfWork uof)
        {
            Database = uof;
        }

        public void DeleteGroup(int id)
        {
            if (id == 0)
                throw new Exception("id == null");

            Database.Groups.Delete(id);
            Database.Save();
        }

        //public void Dispose()
        //{
        //    Database.Dispose();
        //}

        public GroupDTO GetGroup(int? id)
        {
            if (id == null)
                throw new Exception("id == null");

            var group = Database.Groups.Get(id.Value);

            if (group == null)
                throw new Exception("group don't find");

            return new GroupDTO
            {
                Id = group.Id,
                GroupName = group.GroupName
            };
        }

        public IEnumerable<GroupDTO> GetGroups()
        {
            var groups = Database.Groups.GetAll();
            List<GroupDTO> groupdto = new List<GroupDTO>();

            if (groups == null)
            {
                throw new Exception("Error create list of groups");
            }

            foreach (var g in groups)
            {
                groupdto.Add(new GroupDTO
                {
                    Id = g.Id,
                    GroupName = g.GroupName
                });
            }

            return groupdto;
        }

        public void UpdateGroup(GroupDTO groupDto)
        {
            if (groupDto == null)
            {
                throw new Exception("error. update group bll");
            }

            Groupp group = new Groupp
            {
                Id = groupDto.Id,
                GroupName = groupDto.GroupName
            };

            Database.Groups.Update(group);
            Database.Save();
        }

        public void CreateGroup(GroupDTO groupDto)
        {
            if (groupDto == null)
            {
                throw new Exception("Error. group == null");
            }

            Groupp group = new Groupp
            {
                GroupName = groupDto.GroupName
            };

            Database.Groups.Create(group);
            Database.Save();
        }
    }
}
