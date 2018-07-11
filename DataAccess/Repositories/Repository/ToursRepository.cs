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
    public class ToursRepository : Repository<Tours, int>, IToursRepository
    {
        public override IEnumerable<Tours> FindAll() => Connection.Query<Tours>(StoredProcedures.General.SelectAllFrom + "\"" + nameof(Tours) + "\"");
        public override Tours FindByID(int id) => Connection.Get(new Tours { TourID = id });

    }
}
