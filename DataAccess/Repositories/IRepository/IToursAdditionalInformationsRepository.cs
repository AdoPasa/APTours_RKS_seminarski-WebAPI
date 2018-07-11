using Core.DTO;
using Core.Entities;
using System.Collections.Generic;

namespace DataAccess.Repositories.IRepository
{
    public interface IToursAdditionalInformationsRepository : IRepository<ToursAdditionalInformations, int>
    {
        IEnumerable<ToursAdditionalInformationsDTO> FindByTourID(int tourId);
    }
}
