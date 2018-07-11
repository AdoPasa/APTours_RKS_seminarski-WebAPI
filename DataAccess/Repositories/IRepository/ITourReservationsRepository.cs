using Core.Entities;
using Core;

namespace DataAccess.Repositories.IRepository
{
    public interface ITourReservationsRepository : IRepository<TourReservations, int>
    {
        new bool Add(TourReservations reservation);
        bool Remove(int userId, int tourId, int reservationId);
    }
}
