using Core.Entities;
using Dapper.FastCrud;
using DataAccess.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories.Repository
{
    public class AuthenticationTokensRepository : Repository<AuthenticationTokens, int>, IAuthenticationTokensRepository
    {
        public override IEnumerable<AuthenticationTokens> FindAll()
        {
            throw new NotImplementedException();
        }

        public override AuthenticationTokens FindByID(int id)
        {
            return Connection.Get(new AuthenticationTokens { AuthenticationTokenID = id });
        }

        public IEnumerable<AuthenticationTokens> FindDevicesByUserID(int userId)
        {
            return Connection.Find<AuthenticationTokens>(statement => statement.Where($"{nameof(AuthenticationTokens.UserID):C}=@UserID")
                                                                .WithParameters(new { UserID = userId }));
        }

        public AuthenticationTokens FindByAuthToken(string authToken, bool loadUser = true)
        {
            return Connection.Find<AuthenticationTokens>(statement => statement.Where($"{nameof(AuthenticationTokens.AuthenticationToken):C}=@AuthenticationToken")
                                                                .WithParameters(new { AuthenticationToken = authToken })).FirstOrDefault();
        }

        public IEnumerable<AuthenticationTokens> FindByDeviceToken(string deviceToken)
        {
            return Connection.Find<AuthenticationTokens>(statement => statement.Where($"{nameof(AuthenticationTokens.DeviceToken):C}=@DeviceToken")
                                                                .WithParameters(new { DeviceToken = deviceToken}));
        }

        public bool DeactivateByDeviceToken(string deviceToken) {
            try
            {
                spExecuteWithNoResults(StoredProcedures.AuthenticationTokens.AuthenticationTokensDeactivateByDeviceToken, deviceToken);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeactivateByUserID(int userId)
        {
            try
            {
                spExecuteWithNoResults(StoredProcedures.AuthenticationTokens.AuthenticationTokensDeactivateByUserID, userId);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
