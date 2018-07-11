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
    public class TourAgendaRepository : Repository<TourAgenda, int>, ITourAgendaRepository
    {
        public override IEnumerable<TourAgenda> FindAll() => Connection.Query<TourAgenda>(StoredProcedures.General.SelectAllFrom + "\"" + nameof(TourAgenda) + "\"");
        public override TourAgenda FindByID(int id) => Connection.Get(new TourAgenda { TourAgendaID = id });


        public IEnumerable<TourAgenda> FindByTourID(int tourId)
        {
            return Connection.Find<TourAgenda>(statement => statement.Where($"{nameof(TourAgenda.TourID):C}=@TourID")
                                                                     .WithParameters(new { TourID = tourId }));
        }
    }
}
