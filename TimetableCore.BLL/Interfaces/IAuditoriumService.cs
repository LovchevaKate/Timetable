using System.Collections.Generic;
using TimetableCore.BLL.DTO;

namespace TimetableCore.BLL.Interfaces
{
    public interface IAuditoriumService
    {
        void CreateAuditorium(AuditoriumDTO auditorium);
        AuditoriumDTO GetAuditorium(int? id);
        IEnumerable<AuditoriumDTO> GetAuditoriums();
        void UpdateAuditorium(AuditoriumDTO auditorium);
        void DeleteAuditorium(int id);
    }
}
