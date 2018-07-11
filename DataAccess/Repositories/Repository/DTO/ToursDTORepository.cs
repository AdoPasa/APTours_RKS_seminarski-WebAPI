using Core.DTO;
using DataAccess.Repositories.IRepository.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repositories.Repository.DTO
{
    public class ToursDTORepository : Repository<ToursDTO, int>, IToursDTORepository
    {
        public override IEnumerable<ToursDTO> FindAll()
        {
            throw new NotImplementedException();
        }

        public override ToursDTO FindByID(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ToursDTO> FindActiveByParamaters(int userId, int page, int numberOfResults)
        {
            return spExecute<ToursDTO>(StoredProcedures.Tours.ToursDTOSelectActiveByParameters, userId, page, numberOfResults);
        }

        public IEnumerable<ToursDTO> FindUpcomingByParamaters(int userId, int page, int numberOfResults)
        {
            return spExecute<ToursDTO>(StoredProcedures.Tours.ToursDTOSelectUpcomingByParameters, userId, page, numberOfResults);
        }

        public IEnumerable<ToursDTO> FindSavedByParamaters(int userId, int page, int numberOfResults)
        {
            return spExecute<ToursDTO>(StoredProcedures.Tours.ToursDTOSelectSavedByParameters, userId, page, numberOfResults);
        }

        public IEnumerable<ToursDTO> FindFinishedByParamaters(int userId, int page, int numberOfResults)
        {
            return spExecute<ToursDTO>(StoredProcedures.Tours.ToursDTOSelectFinishedByParameters, userId, page, numberOfResults);
        }        
    }
}
