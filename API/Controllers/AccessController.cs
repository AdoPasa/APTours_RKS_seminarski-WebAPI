using API.Models;
using API.ViewModels;
using Core.Entities;
using Core.Resources;
using DataAccess.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Helpers;
using System.Configuration;
using System.Text.RegularExpressions;
using Core;

namespace API.Controllers
{
    [RoutePrefix("api/Access")]
    public class AccessController : BaseApiController
    {
        #region Properties
        private IUsersRepository _dbUsers;
        private IUserRolesRepository _dbUserRoles;
        private IAuthenticationTokensRepository _dbAuthenticationTokens;

        public AccessController(IUsersRepository dbUsers, IRolesRepository dbRoles, IAuthenticationTokensRepository dbAuthenticationTokens,
            IUserRolesRepository dbUserRoles) : base(dbUsers, dbRoles)
        {
            _dbUsers = dbUsers;
            _dbAuthenticationTokens = dbAuthenticationTokens;
            _dbUserRoles = dbUserRoles;
        }
        #endregion

        #region GetAccess(information)
        public IHttpActionResult GetAccess()
        {
            string t = GetAuthToken();
            AuthenticationTokens authenticationToken = _dbAuthenticationTokens.FindByAuthToken(t, false);

            if (authenticationToken?.UserID == null)
                return Unauthorized();
                    
            Users user = _dbUsers.FindByID(authenticationToken.UserID);

            return Ok(new VMAuthentication {
                        UserID = user.UserID,
                        AuthToken = authenticationToken.AuthenticationToken,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Username = user.Username,
                        Email = user.Email,
                        ProfilePhoto = user.ProfilePhoto
            });
        }
        #endregion

        #region Login
        [HttpPost]
        [Route("Login")]
        public IHttpActionResult Login([FromBody]VMLogin model)
        {
            Users user = FindUser(model.Username, model.Password);

            if (user == null)
                return NotFound(); 

            if (!user.Active) {
                if (user.ActivationCode == null)
                    return StatusCode(HttpStatusCode.Unauthorized);
                else
                    return StatusCode(HttpStatusCode.Ambiguous);
            }

            _dbAuthenticationTokens.DeactivateByDeviceToken(model.DeviceToken);

            string generated = Guid.NewGuid().ToString();
            AuthenticationTokens authenticationToken = new AuthenticationTokens
            {
                UserID = user.UserID,
                DateTimeCreated = DateTime.Now,
                DeviceToken = model.DeviceToken,
                AuthenticationToken = generated,
                Info_Version_Release = model.InfoVersionRelease,
                Info_Device = model.InfoDevice,
                Info_Model = model.InfoModel,
                Info_Product = model.InfoProduct,
                Info_Brand = model.InfoBrand,
                Info_Manufacturer = model.InfoManufacturer,
                Android_SerialOrID = model.AndroidSerialOrID
            };
            _dbAuthenticationTokens.Add(authenticationToken);

            return Ok(new VMAuthentication {
                        UserID = user.UserID,
                        AuthToken = generated,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Username = model.Username,
                        Email = user.Email,
                        ProfilePhoto = user.ProfilePhoto
            });
        }
        #endregion

        #region Logout
        [HttpGet]
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            RemoveAuthToken();
            return StatusCode(HttpStatusCode.NoContent);
        }
        #endregion

        #region RefreshDeviceToken -- treba prapravit 
        [HttpGet]
        [Route("RefreshDeviceToken")]
        public HttpResponseMessage RefreshDeviceToken([FromUri] string newDeviceToken)
        {
            Users user = GetUserOfAuthToken();

            if (user == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ApiResult<VMAuthentication>.Error(401, Resource.ErrMsgUnauthorizedAccess));

            RemoveAuthToken();
            return Request.CreateResponse(HttpStatusCode.OK, AddAuthToken(newDeviceToken, user));
        }
        #endregion

        #region Register
        [HttpPost]
        [Route("Register")]
        public IHttpActionResult Register([FromBody]VMRegister model) {
            if (GetUserOfAuthToken() != null)
                return StatusCode(HttpStatusCode.BadRequest);

            if (_dbUsers.FindByEmail(model.Email) != null)
                return StatusCode((HttpStatusCode)550);

            if (_dbUsers.FindByPhone(model.Phone) != null)
                return StatusCode((HttpStatusCode)551);

            if (_dbUsers.FindByUsername(model.Username) != null)
                return StatusCode((HttpStatusCode)552);


            if (!ModelState.IsValid)
                return BadRequest();

            string salt = PasswordGenerator.GenerateSalt();
            Users user = new Users {
                FirstName = model.FirstName,
                LastName = model.LastName,
                ProfilePhoto = null,
                Username = model.Username,
                Email = model.Email,
                Phone = model.Phone,
                PasswordSalt = salt,
                PasswordHash = PasswordGenerator.GenerateHash(model.Password, salt),
                Gender = model.Gender,
                Active = false,
                ActivationCode = PasswordGenerator.GenerateRandomAlphanumericStringWithoutSpecialChar(10),
                CreatedDate = DateTime.Now
            };

            if(!_dbUsers.Add(user))
                return StatusCode((HttpStatusCode)554);

            UserRoles role = new UserRoles { UserID = user.UserID, RoleID = Convert.ToInt32(Enumerations.Roles.User) };

            if (!_dbUserRoles.Add(role)) {
                _dbUsers.Remove(user);

                return StatusCode((HttpStatusCode)554);
            }

            string body = string.Format(Resource.EmailMsgRegistration, user.FirstName, DateTime.Now.ToString(Resource.DateFormat) + " " + Resource.AtTime + " " + DateTime.Now.TimeOfDay.ToString(@"hh\:mm") + " h",
                                        Resource.ProjectName, user.ActivationCode);

            try {
                MailHelper.Send(Resource.EmailTitleRegistration, body, ConfigurationManager.AppSettings["noReplyMail"], user.Email);
                return Ok();
            }
            catch (Exception ex) {
                _dbUserRoles.Remove(role);
                _dbUsers.Remove(user);

                return StatusCode((HttpStatusCode)555);
            }
        }
        #endregion

        #region ActivateAccount
        [HttpPost]
        [Route("ActivateAccount", Name = "ActivateAccount")]
        public IHttpActionResult ActivateAccount([FromBody]VMActivateAccount model) {
            Users user = _dbUsers.FindByUsername(model.Username);

            if (user == null || user.Active == true || user.ActivationCode == null)
                return NotFound();

            if(model.ActivationCode != user.ActivationCode)
                return BadRequest();

            user.ActivationCode = null;
            user.Active = true;

            if(!_dbUsers.Edit(user))
                return BadRequest();

            return Ok();
        }
        #endregion

        #region SendResetPasswordCode
        [HttpPost]
        [Route("SendResetPasswordCode")]
        public IHttpActionResult SendResetPasswordCode([FromBody]string Email)
        {
            Users user = GetUserOfAuthToken();

            if (user != null)
                return StatusCode(HttpStatusCode.Moved);

            user = _dbUsers.FindByEmail(Email);

            if (user == null)
                return NotFound();

            user.ResetPasswordTokenExpiration = DateTime.Now.AddMinutes(20);
            user.ResetPasswordToken = PasswordGenerator.GenerateRandomAlphanumericStringWithoutSpecialChar(10);

            if (!_dbUsers.Edit(user))
                return StatusCode((HttpStatusCode)554);

            string body = string.Format(user.Gender == Resource.ShortGenderMale ? Resource.InfoEmailMsgForgetPasswordMale : Resource.InfoEmailMsgForgetPasswordFemale,
                   user.FirstName, DateTime.Now.ToString(Resource.DateFormat) + " " + Resource.AtTime + " " + DateTime.Now.TimeOfDay.ToString(@"hh\:mm") + " h",
                   Resource.ProjectName, user.ResetPasswordToken);

            try {
                MailHelper.Send(Resource.EmailTitleRequestForPasswordRecovery, body, ConfigurationManager.AppSettings["noReplyMail"], Email);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((HttpStatusCode)555);
            }
        }
        #endregion

        #region ResetPassword
        [HttpPost]
        [Route("ResetPassword")]
        public IHttpActionResult ResetPassword(VMResetPassword model)
        {
            Users user = _dbUsers.FindByResetPasswordToken(model.ResetPasswordToken);

            if (user == null)
                return NotFound();

            Regex passwordRule = new Regex(@"(?=^.{6,}$)(?=.*\d)(?=.*[a-zA-Z])");

            if (string.IsNullOrEmpty(model.NewPassword) || model.NewPassword != model.ConfirmNewPassword || !passwordRule.IsMatch(model.NewPassword))
                return BadRequest();


            if (!_dbAuthenticationTokens.DeactivateByUserID(user.UserID))
                return StatusCode((HttpStatusCode)456);

            user.PasswordSalt = PasswordGenerator.GenerateSalt();
            user.PasswordHash = PasswordGenerator.GenerateHash(model.NewPassword, user.PasswordSalt);
            user.LastPasswordChange = DateTime.Now;
            user.ResetPasswordToken = null;
            user.ResetPasswordTokenExpiration = null;

            if (!_dbUsers.Edit(user))
                return StatusCode((HttpStatusCode)554);

            return Ok();
        }
        #endregion

        #region Helpers
        private Users FindUser(string username, string password)
        {
            Users user = _dbUsers.FindByUsername(username?.Trim());

            if (user == null)
                return null;

            if (PasswordGenerator.GenerateHash(password, user.PasswordSalt) == user.PasswordHash)
                return user;

            return null;
        }

        private ApiResult<VMAuthentication> AddAuthToken(string deviceToken, Users user)
        {
            if (user == null)
                return ApiResult<VMAuthentication>.Error(1, Resource.IncorrectLoginCredentials);

            if (!user.Active)
                return ApiResult<VMAuthentication>.Error(1, Resource.ErrMsgUserAccountNotActive);

            List<AuthenticationTokens> tokensToDelete = _dbAuthenticationTokens.FindByDeviceToken(deviceToken).ToList();

            foreach (var token in tokensToDelete)
            {
                token.IsDeleted = true;
                token.DateTimeDeleted = DateTime.Now;
                _dbAuthenticationTokens.Edit(token);
            }

            string generated = Guid.NewGuid().ToString();
            AuthenticationTokens authenticationToken = new AuthenticationTokens
            {
                UserID = user.UserID,
                DateTimeCreated = DateTime.Now,
                DeviceToken = deviceToken,
                AuthenticationToken = generated,
            };
            _dbAuthenticationTokens.Add(authenticationToken);

            return ApiResult<VMAuthentication>.OK(new VMAuthentication
            {
                UserID = user.UserID,
                AuthToken = generated,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email,
                ProfilePhoto = user.ProfilePhoto
            });
        }

        private void RemoveAuthToken()
        {
            string t = GetAuthToken();

            AuthenticationTokens token = _dbAuthenticationTokens.FindByAuthToken(t);

            if (token != null)
            {
                token.IsDeleted = true;
                token.DateTimeDeleted = DateTime.Now;
                _dbAuthenticationTokens.Edit(token);
            }
        }
        #endregion
    }
}
