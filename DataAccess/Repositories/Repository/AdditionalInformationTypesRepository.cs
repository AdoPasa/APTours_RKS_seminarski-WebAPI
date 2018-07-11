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
    public class AdditionalInformationTypesRepository : Repository<AdditionalInformationTypes, int>, IAdditionalInformationTypesRepository
    {
        public override IEnumerable<AdditionalInformationTypes> FindAll() => Connection.Query<AdditionalInformationTypes>(StoredProcedures.General.SelectAllFrom + "\"" + nameof(AdditionalInformationTypes) + "\"");
        public override AdditionalInformationTypes FindByID(int id) => Connection.Get(new AdditionalInformationTypes { AdditionalInformationTypeID = id });

    }
}
