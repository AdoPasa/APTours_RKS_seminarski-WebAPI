using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repositories.IRepository
{
    public interface IUserRolesRepository:IRepository<UserRoles,int>
    {
        int CountByRoleID(int roleId);
    }
}
