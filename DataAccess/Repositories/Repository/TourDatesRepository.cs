using Core;
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
    public class TourDatesRepository : Repository<TourDates, int>, ITourDatesRepository
    {
        public override IEnumerable<TourDates> FindAll() => Connection.Query<TourDates>(StoredProcedures.General.SelectAllFrom + "\"" + nameof(TourDates) + "\"");
        public override TourDates FindByID(int id) => Connection.Get(new TourDates { TourDateID = id });

        public IEnumerable<TourDates> FindActiveByTourID(int tourId) {
            return spExecute<TourDates>(StoredProcedures.TourDates.TourDatesSelectActiveByTourID, tourId);
        }
    }
}
