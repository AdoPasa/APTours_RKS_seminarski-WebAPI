using Core.Entities;
using System.Collections.Generic;

namespace DataAccess.Repositories.IRepository
{
    public interface ITourAgendaRepository : IRepository<TourAgenda, int>
    {
        IEnumerable<TourAgenda> FindByTourID(int tourId);
    }
}
