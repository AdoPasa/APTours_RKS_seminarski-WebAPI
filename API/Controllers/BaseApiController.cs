using Core;
using Core.Entities;
using DataAccess.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;

namespace API.Controllers
{
    public class BaseApiController : ApiController
    {
        #region Properties
        private IUsersRepository _dbUsers{ get; }
        private IRolesRepository _dbRoles{ get; }

        public BaseApiController(IUsersRepository dbUsers, IRolesRepository dbRoles)
        {
            _dbUsers = dbUsers;
            _dbRoles = dbRoles;

            /*NameValueCollection headers = HttpContext.Current.Request.Headers;

            if (headers["language"] != null)
            {
                language = headers["language"];
            }*/
        }
        #endregion

        #region CurrentUser
        public Users CurrentUser { get { return GetUserOfAuthToken(_dbUsers, _dbRoles, false); } }
        #endregion

        #region GetUserOfAuthToken
        [NonAction]
        public Users GetUserOfAuthToken(bool allRoles, params Enumerations.Roles[] roles)
        {
            return GetUserOfAuthToken(_dbUsers, _dbRoles, allRoles, roles);
        }

        [NonAction]
        public Users GetUserOfAuthToken(params Enumerations.Roles[] roles)
        {
            return GetUserOfAuthToken(_dbUsers, _dbRoles, false, roles);
        }
        #endregion

        #region IsUserLogged
        [NonAction]
        public bool IsUserLogged() {
            return CurrentUser != null;
        }
        #endregion

        #region Helpers
        public string GetControllerName { get { return ControllerContext.ControllerDescriptor.ControllerName; } }

        public static Users GetUserOfAuthToken(IUsersRepository dbUsers, IRolesRepository dbRoles, bool allRoles, params Enumerations.Roles[] roles)
        {
            Users user = dbUsers.FindByAuthToken(GetAuthToken());

            if (user == null)
                return null;
            
            if (roles == null || roles.Length == 0)
                return user;

            List<Roles> currentUserRoles = dbRoles.FindByUserID(user.UserID).ToList();

            if (!allRoles)
            {
                foreach (var item in roles)
                {
                    if (currentUserRoles.Any(x => x.RoleID == Convert.ToInt32(item)))
                        return user;
                }

                return null;
            }

            foreach (var item in roles)
            {
                if (!currentUserRoles.Any(x => x.RoleID == Convert.ToInt32(item)))
                    return null;
            }

            return user;
        }

        protected static string GetAuthToken()
        {
            string authToken = null;
            NameValueCollection headers = HttpContext.Current.Request.Headers;

            if (headers["AuthToken"] != null)
                authToken = headers["AuthToken"];

            return authToken;
        }
        #endregion
    }
}
