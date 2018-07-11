using Core;
using Core.Entities;
using Core.Extensions;
using Dapper;
using Dapper.FastCrud;
using DataAccess.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repositories.Repository
{
    public class UsersRepository : Repository<Users, int>, IUsersRepository
    {
        public override IEnumerable<Users> FindAll() => Connection.Query<Users>(StoredProcedures.General.SelectAllFrom + "\"" + nameof(Users) + "\"");

        public override Users FindByID(int id) => Connection.Get(new Users { UserID = id });

        public Users FindByUsername(string username)
        {
            return Connection.Find<Users>(statement => statement.Where($"{nameof(Users.Username):C}=@username")
                                                                .WithParameters(new { username = username })).FirstOrDefault();
        }

        public Users FindByAuthToken(string authToken)
        {
            return spExecute<Users>(StoredProcedures.Users.UsersSelectByAuthToken, authToken).FirstOrDefault();
        }

        public void SetLastAccess(int id)
        {
            try
            {
                //Ne znam iz kojeg razloga ne radi!!!
                //Pogledati poslije
                //int up = Connection.BulkUpdate(new Users { LastAccess = DateTime.Now }, x => x.Where($"{nameof(Users.UserID):C}=1"));//.WithParameters(new { UserID = id }));

                Users user = FindByID(id);
                user.LastAccess = DateTime.Now;

                Connection.Update(user);
            }
            catch (Exception ex)
            {
            }
        }

        public Users FindByEmail(string email) {
            return Connection.Find<Users>(statement => statement.Where($"{nameof(Users.Email):C}=@Email")
                                                                .WithParameters(new { Email = email })).FirstOrDefault();
        }

        public Users FindByPhone(string phone)
        {
            return Connection.Find<Users>(statement => statement.Where($"{nameof(Users.Phone):C}=@Phone")
                                                                .WithParameters(new { Phone = phone })).FirstOrDefault();
        }

        public Users FindByResetPasswordToken(string token)
        {
            if (string.IsNullOrEmpty(token.Trim()))
                return null;

            return Connection.Find<Users>(statement => statement.Where($"{nameof(Users.ResetPasswordToken):C}=@token AND {nameof(Users.ResetPasswordTokenExpiration):C}>@currentDate")
                                                                .WithParameters(new { token = token, currentDate = DateTime.Now })).FirstOrDefault();
        }

    }
}
