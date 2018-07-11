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
    public class ToursAdditionalInformationsRepository : Repository<ToursAdditionalInformations, int>, IToursAdditionalInformationsRepository
    {
        public override IEnumerable<ToursAdditionalInformations> FindAll() => Connection.Query<ToursAdditionalInformations>(StoredProcedures.General.SelectAllFrom + "\"" + nameof(ToursAdditionalInformations) + "\"");
        public override ToursAdditionalInformations FindByID(int id) => Connection.Get(new ToursAdditionalInformations { TourAdditionalInformationID = id });

        public IEnumerable<ToursAdditionalInformationsDTO> FindByTourID(int tourId) {
            return spExecute<ToursAdditionalInformationsDTO>(StoredProcedures.ToursAdditionalInformations.ToursAdditionalInformationsDTOSelectByTourID, tourId);
        }
    }
}
