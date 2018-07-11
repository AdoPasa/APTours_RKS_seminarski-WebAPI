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
    public class FavoriteToursRepository : Repository<FavoriteTours, int>, IFavoriteToursRepository
    {
        public override IEnumerable<FavoriteTours> FindAll() => Connection.Query<FavoriteTours>(StoredProcedures.General.SelectAllFrom + "\"" + nameof(FavoriteTours) + "\"");
        public override FavoriteTours FindByID(int id) => null;
    }
}
