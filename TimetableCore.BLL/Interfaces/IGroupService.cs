using System.Collections.Generic;
using TimetableCore.BLL.DTO;

namespace TimetableCore.BLL.Interfaces
{
    public interface IGroupService
    {
        void CreateGroup(GroupDTO group);
        GroupDTO GetGroup(int? id);
        IEnumerable<GroupDTO> GetGroups();
        void UpdateGroup(GroupDTO group);
        void DeleteGroup(int id);
    }
}
