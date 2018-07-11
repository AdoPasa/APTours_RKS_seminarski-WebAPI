using Core;
using Core.DTO;
using Core.Entities;
using Core.Extensions;
using Dapper;
using Dapper.FastCrud;
using DataAccess.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repositories.Repository
{
    public class TourReviewsRepository : Repository<TourReviews, int>, ITourReviewsRepository
    {
        public override IEnumerable<TourReviews> FindAll() => Connection.Query<TourReviews>(StoredProcedures.General.SelectAllFrom + "\"" + nameof(TourReviews) + "\"");
        public override TourReviews FindByID(int id) => Connection.Get(new TourReviews { TourReviewID = id });

        public IEnumerable<TourReviewsDTO> FindByTourID(int tourId)
        {
            return spExecute<TourReviewsDTO>(StoredProcedures.TourReviews.TourReviewsDTOSelectByTourID, tourId);
        }

        new public bool Add(TourReviews review) {
            try
            {
                spExecuteWithNoResults(StoredProcedures.TourReviews.TourReviewsInsert, 
                    review.UserID,
                    review.TourID,
                    review.Grade,
                    review.Review
                );

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
