using Core.DTO;
using Core.Entities;
using System.Collections.Generic;

namespace DataAccess.Repositories.IRepository
{
    public interface ITourReviewsRepository : IRepository<TourReviews, int>
    {
        IEnumerable<TourReviewsDTO> FindByTourID(int tourId);
        new bool Add(TourReviews review);
    }
}
