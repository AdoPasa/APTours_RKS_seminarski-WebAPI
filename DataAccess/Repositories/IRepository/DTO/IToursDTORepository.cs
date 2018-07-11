using Core.DTO;
using System.Collections.Generic;

namespace DataAccess.Repositories.IRepository.DTO
{
    public interface IToursDTORepository : IRepository<ToursDTO, int>
    {
        IEnumerable<ToursDTO> FindActiveByParamaters(int userId, int page, int numberOfResults);
        IEnumerable<ToursDTO> FindUpcomingByParamaters(int userId, int page, int numberOfResults);
        IEnumerable<ToursDTO> FindSavedByParamaters(int userId, int page, int numberOfResults);
        IEnumerable<ToursDTO> FindFinishedByParamaters(int userId, int page, int numberOfResults);        
    }
}
