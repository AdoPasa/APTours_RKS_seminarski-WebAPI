using API.Helpers;
using API.Helpers.FileManager;
using API.ViewModels;
using Core.Entities;
using DataAccess.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("api/Users")]
    public class UsersController : BaseApiController
    {
        IUsersRepository _dbUsers;
        IFileManager _fileManager;

        public UsersController(IUsersRepository dbUsers, IRolesRepository dbRoles,
            IFileManager fileManager) :base(dbUsers, dbRoles)
        {
            _dbUsers = dbUsers;
            _fileManager = fileManager;
        }

        //Kontam da get vraća podatke profila
        [HttpPost]
        [Route("ChangePassword", Name = "ChangePassword")]
        public HttpResponseMessage ChangePassword([FromBody]string password) {
            return null;
        }

        [HttpGet]
        [Route("Profile")]
        public IHttpActionResult GetPofile() {
            Users currentUser = GetUserOfAuthToken();

            if (currentUser == null)
                return Unauthorized();

            return Ok(currentUser);
        }

        [HttpPost]
        [Route("Profile")]
        public IHttpActionResult UpdatePofile(VMUpdateProfile model)
        {
            bool userModified = false;
            Users currentUser = GetUserOfAuthToken();
            
            if (currentUser == null)
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest();

            if(currentUser.PasswordHash != PasswordGenerator.GenerateHash(model.CurrentPassword, currentUser.PasswordSalt))
                return StatusCode((HttpStatusCode)540);

            if (currentUser.Email != model.Email) {
                if(_dbUsers.FindByEmail(model.Email) != null)
                    return StatusCode((HttpStatusCode)550);

                currentUser.Email = model.Email;
                userModified = true;
            }

            if (currentUser.Phone != model.Phone) {
                if(_dbUsers.FindByPhone(model.Phone) != null)
                    return StatusCode((HttpStatusCode)551);

                currentUser.Phone = model.Phone;
                userModified = true;
            }

            if (!string.IsNullOrEmpty(model.NewPassword)) {
                Regex exp = new Regex(@"^((?=.*[a-zA-Z])(?=.*\d)).{6,}$");

                if(!exp.IsMatch(model.NewPassword) || model.NewPassword != model.ConfirmNewPassword)
                    return BadRequest();

                currentUser.PasswordSalt = PasswordGenerator.GenerateSalt();
                currentUser.PasswordHash = PasswordGenerator.GenerateHash(model.NewPassword, currentUser.PasswordSalt);
                currentUser.LastPasswordChange = DateTime.Now;

                userModified = true;
            }

            if (currentUser.FirstName != model.FirstName || currentUser.LastName != model.LastName || currentUser.Gender != model.Gender) {
                currentUser.FirstName = model.FirstName;
                currentUser.LastName = model.LastName;
                currentUser.Gender = model.Gender;
                userModified = true;
            }

            if (userModified) { 
                if(!_dbUsers.Edit(currentUser))
                    return InternalServerError();
            }

            return Ok();            
        }

        [HttpPost]
        [Route("ProfilePhoto")]
        public IHttpActionResult UpdateProfilePhoto(VMUpdateProfilePhoto model) {
            if (CurrentUser == null)
                return Unauthorized();

            if(model.ImageStream == null)
                return BadRequest();

            Users user = CurrentUser;
            string imageType = ".jpeg";
            Guid streamId = _fileManager.Save(new MemoryStream(model.ImageStream), null, imageType, "~/Images/Users/");

            if (streamId != Guid.Empty)
            {
                string previusPhoto = user.ProfilePhoto?.Split('/').Last();

                user.ProfilePhoto = $"Images/Users/{streamId.ToString()}{imageType}";
                _dbUsers.Edit(user);

                if(previusPhoto != null)
                    _fileManager.Delete("~/Images/Users/", previusPhoto);
            }
            else
                return BadRequest();

            return Ok(user.ProfilePhoto);
        }
    }
}
