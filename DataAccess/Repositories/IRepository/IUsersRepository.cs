using Core.Entities;
using Core;

namespace DataAccess.Repositories.IRepository
{
    public interface IUsersRepository : IRepository<Users, int>
    {
        Users FindByUsername(string username);
        Users FindByPhone(string phone);
        Users FindByAuthToken(string authToken);
        void SetLastAccess(int id);
        Users FindByEmail(string email);
        Users FindByResetPasswordToken(string token);
    }
}
