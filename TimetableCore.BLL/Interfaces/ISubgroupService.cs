using System.Collections.Generic;
using TimetableCore.BLL.DTO;

namespace TimetableCore.BLL.Interfaces
{
    public interface ISubgroupService
    {
        void CreateSubgroup(SubgroupDTO subgroup);
        SubgroupDTO GetSubgroup(int? id);
        IEnumerable<SubgroupDTO> GetSubgroups();
        void UpdateSubgroup(SubgroupDTO subgroup);
        void DeleteSubgroup(int id);
        //void Dispose();
    }
}
