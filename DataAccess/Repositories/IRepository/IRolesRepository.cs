using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repositories.IRepository
{
    public interface IRolesRepository:IRepository<Roles,int>
    {
        bool Remove(int id);
        IEnumerable<Roles> FindActive();
        IEnumerable<Roles> FindByUserID(int userId);
    }
}
