using Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repositories.IRepository.DTO
{
    public interface IUsersDTORepository: IRepository<UsersDTO, int>
    {
        //IEnumerable<UsersDTO> FindByParameters(string firstName, string lastName, bool active, int resultsPerPage, int offset);
    }
}
