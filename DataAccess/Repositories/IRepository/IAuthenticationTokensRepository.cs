using Core.Entities;
using System.Collections.Generic;

namespace DataAccess.Repositories.IRepository
{
    public interface IAuthenticationTokensRepository : IRepository<AuthenticationTokens, int>
    {
        AuthenticationTokens FindByAuthToken(string authToken, bool loadUser = true);
        IEnumerable<AuthenticationTokens> FindDevicesByUserID(int userId);
        IEnumerable<AuthenticationTokens> FindByDeviceToken(string deviceToken);
        bool DeactivateByUserID(int userId);
        bool DeactivateByDeviceToken(string deviceToken);
    }
}
