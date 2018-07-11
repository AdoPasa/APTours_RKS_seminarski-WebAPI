using Core.DTO;
using DataAccess.Repositories.IRepository.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repositories.Repository.DTO
{
    public class UsersDTORepository : Repository<UsersDTO, int>, IUsersDTORepository
    {
        public override IEnumerable<UsersDTO> FindAll()
        {
            throw new NotImplementedException();
        }

        public override UsersDTO FindByID(int id)
        {
            throw new NotImplementedException();
        }

       /* public IEnumerable<UsersDTO> FindByParameters(string firstName, string lastName, bool active, int resultsPerPage, int offset) {
            return fnExecute<UsersDTO>(Functions.Users.UsersDTOSelectByParameters, firstName, lastName, active, resultsPerPage, offset);
        }*/
    }
}
