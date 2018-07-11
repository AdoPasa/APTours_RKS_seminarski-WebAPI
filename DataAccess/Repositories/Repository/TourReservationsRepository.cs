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
    public class TourReservationsRepository : Repository<TourReservations, int>, ITourReservationsRepository
    {
        public override IEnumerable<TourReservations> FindAll() => Connection.Query<TourReservations>(StoredProcedures.General.SelectAllFrom + "\"" + nameof(TourReservations) + "\"");
        public override TourReservations FindByID(int id) => Connection.Get(new TourReservations { TourReservationID = id });

        new public bool Add(TourReservations reservation)
        {
            try
            {
                spExecuteWithNoResults(StoredProcedures.TourReservations.TourReservationsInsert,
                    reservation.UserID,
                    reservation.TourID,
                    reservation.TourDateID,
                    reservation.NumberOfPassengers
                );

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Remove(int userId, int tourId, int reservationId) {
            try {
                spExecuteWithNoResults(StoredProcedures.TourReservations.TourReservationsDelete, userId, tourId, reservationId);
                return true;
            }
            catch (Exception ex) { return false; }
        }
    }
}
