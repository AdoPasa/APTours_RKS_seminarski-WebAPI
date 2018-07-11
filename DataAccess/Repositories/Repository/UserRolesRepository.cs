using Core.Entities;
using Dapper.FastCrud;
using DataAccess.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repositories.Repository
{
    public class UserRolesRepository : Repository<UserRoles, int>, IUserRolesRepository
    {
        public int CountByRoleID(int roleId)
        {
            return Connection.Count<UserRoles>(x => x.Where($"{nameof(UserRoles.RoleID):C}=@roleId").WithParameters(new { roleId=roleId }));
        }

        public override IEnumerable<UserRoles> FindAll()
        {
            throw new NotImplementedException();
        }

        public override UserRoles FindByID(int id)
        {
            throw new NotImplementedException();
        }
    }
}
