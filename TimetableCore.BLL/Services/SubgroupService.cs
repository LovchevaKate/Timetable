using System;
using System.Collections.Generic;
using TimetableCore.BLL.DTO;
using TimetableCore.BLL.Interfaces;
using TimetableCore.DAL.Entities;
using TimetableCore.DAL.Repositories;

namespace TimetableCore.BLL.Services
{
    public class SubgroupService : ISubgroupService
    {
        EFUnitOfWork Database { get; set; }

        public SubgroupService(EFUnitOfWork uof)
        {
            Database = uof;
        }
        public void CreateSubgroup(SubgroupDTO subgroupDto)
        {
            if (subgroupDto == null)
            {
                throw new Exception("Error. subgroup == null");
            }

            Subgroup subgroup = new Subgroup
            {
                SubgroupNumber = subgroupDto.SubgroupNumber
            };

            Database.Subgroups.Create(subgroup);
            Database.Save();
        }

        public void DeleteSubgroup(int id)
        {
            if (id == 0)
                throw new Exception("id == null");

            Database.Subgroups.Delete(id);
            Database.Save();
        }

        //public void Dispose()
        //{
        //    Database.Dispose();
        //}

        public SubgroupDTO GetSubgroup(int? id)
        {
            if (id == null)
                throw new Exception("id == null");

            var subgroup = Database.Subgroups.Get(id.Value);

            if (subgroup == null)
                throw new Exception("subgroup don't find");

            return new SubgroupDTO
            {
                Id = subgroup.Id,
                SubgroupNumber = subgroup.SubgroupNumber
            };
        }

        public IEnumerable<SubgroupDTO> GetSubgroups()
        {
            var subgroups = Database.Subgroups.GetAll();
            List<SubgroupDTO> subgroupdto = new List<SubgroupDTO>();

            if (subgroups == null)
            {
                throw new Exception("Error create list of subgroups");
            }

            foreach (var s in subgroups)
            {
                subgroupdto.Add(new SubgroupDTO
                {
                    Id = s.Id,
                    SubgroupNumber = s.SubgroupNumber
                });
            }

            return subgroupdto;
        }

        public void UpdateSubgroup(SubgroupDTO subgroupDto)
        {
            if (subgroupDto == null)
            {
                throw new Exception("error. update subgroup bll");
            }

            Subgroup subgroup = new Subgroup
            {
                Id = subgroupDto.Id,
                SubgroupNumber = subgroupDto.SubgroupNumber
            };

            Database.Subgroups.Update(subgroup);
            Database.Save();
        }
    }
}
