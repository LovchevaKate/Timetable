using System.Collections.Generic;
using TimetableCore.BLL.DTO;

namespace TimetableCore.BLL.Interfaces
{
    public interface IRoleService
    {
        void CreateRole(RoleDTO role);
        RoleDTO GetRole(int? id);
        IEnumerable<RoleDTO> GetRoles();
        void UpdateRole(RoleDTO role);
        void DeleteRole(int id);
        //void Dispose();
    }
}
