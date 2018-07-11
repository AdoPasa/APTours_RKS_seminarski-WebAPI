using Core.Entities;
using Dapper;
using Dapper.FastCrud;
using DataAccess.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repositories.Repository
{
    public class RolesRepository : Repository<Roles, int>, IRolesRepository
    {
        public override IEnumerable<Roles> FindAll() => Connection.Query<Roles>(StoredProcedures.General.SelectAllFrom + "\"" + nameof(Roles) + "\"");

        public override Roles FindByID(int id) => Connection.Get(new Roles { RoleID = id });

        public bool Remove(int id)
        {
            try
            {
                spExecuteWithNoResults(StoredProcedures.Roles.RolesDeleteByRoleID, id);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<Roles> FindActive()
        {
            return Connection.Find<Roles>(statement => statement.Where($"{nameof(Roles.Active):C}=true"));
        }

        public IEnumerable<Roles> FindByUserID(int userId) {
            return spExecute<Roles>(StoredProcedures.Roles.RolesSelectByUserID, userId);
        } 
    }
}
