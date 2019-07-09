using System;
using System.Collections.Generic;
using TimetableCore.BLL.DTO;
using TimetableCore.BLL.Interfaces;
using TimetableCore.DAL.Entities;
using TimetableCore.DAL.Repositories;

namespace TimetableCore.BLL.Services
{
    public class AuditoriumService : IAuditoriumService
    {
        EFUnitOfWork Database { get; set; }

        public AuditoriumService(EFUnitOfWork uof)
        {
            Database = uof;
        }
        public void CreateAuditorium(AuditoriumDTO auditoriumDto)
        {
            if (auditoriumDto == null)
            {
                throw new Exception("Error. user == null");
            }
            Auditorium auditorium = new Auditorium
            {
                AuditoriumCopasity = auditoriumDto.AuditoriumCopasity,
                AuditoriumName = auditoriumDto.AuditoriumName,
                AuditoriumType = auditoriumDto.AuditoriumType
            };

            Database.Auditoriums.Create(auditorium);
            Database.Save();
        }

        public void DeleteAuditorium(int id)
        {
            if (id == 0)
                throw new Exception("id == null");

            Database.Auditoriums.Delete(id);
            Database.Save();
        }

        //public void Dispose()
        //{
        //    Database.Dispose();
        //}

        public AuditoriumDTO GetAuditorium(int? id)
        {
            if (id == null)
                throw new Exception("id == null");

            var auditorium = Database.Auditoriums.Get(id.Value);

            if (auditorium == null)
                throw new Exception("User don't find");

            return new AuditoriumDTO
            {
                Id = auditorium.Id,
                AuditoriumCopasity = auditorium.AuditoriumCopasity,
                AuditoriumName = auditorium.AuditoriumName,
                AuditoriumType = auditorium.AuditoriumType
            };
        }

        public IEnumerable<AuditoriumDTO> GetAuditoriums()
        {
            var auditoriums = Database.Auditoriums.GetAll();
            List<AuditoriumDTO> auditoriumDto = new List<AuditoriumDTO>();

            if (auditoriums == null)
            {
                throw new Exception("Error create list of users");
            }

            foreach (var a in auditoriums)
            {
                auditoriumDto.Add(new AuditoriumDTO
                {
                    Id = a.Id,
                    AuditoriumCopasity = a.AuditoriumCopasity,
                    AuditoriumName = a.AuditoriumName,
                    AuditoriumType = a.AuditoriumType
                });
            }

            return auditoriumDto;
        }

        public void UpdateAuditorium(AuditoriumDTO auditoriumDto)
        {
            if (auditoriumDto == null)
            {
                throw new Exception("error. update user bll");
            }

            Auditorium auditorium = new Auditorium
            {
                Id = auditoriumDto.Id,
                AuditoriumCopasity = auditoriumDto.AuditoriumCopasity,
                AuditoriumName = auditoriumDto.AuditoriumName,
                AuditoriumType = auditoriumDto.AuditoriumType
            };

            Database.Auditoriums.Update(auditorium);
            Database.Save();
        }
    }
}
