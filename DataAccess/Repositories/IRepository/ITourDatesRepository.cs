using Core.Entities;
using Core;
using System.Collections.Generic;

namespace DataAccess.Repositories.IRepository
{
    public interface ITourDatesRepository : IRepository<TourDates, int>
    {
        IEnumerable<TourDates> FindActiveByTourID(int tourId);
    }
}