using Core.DTO;
using DataAccess.Repositories.IRepository.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repositories.Repository.DTO
{
    public class UsersProfileRepositoryDTO : Repository<UsersProfileDTO, int>, IUsersProfileRepositoryDTO
    {
        public override IEnumerable<UsersProfileDTO> FindAll()
        {
            throw new NotImplementedException();
        }

        public override UsersProfileDTO FindByID(int id)
        {
            return spExecute<UsersProfileDTO>(StoredProcedures.Users.UsersProfileDTOSelectByUserID, id).FirstOrDefault();
        }
    }
}
