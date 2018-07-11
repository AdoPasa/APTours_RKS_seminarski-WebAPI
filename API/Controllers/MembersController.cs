using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Core.Entities;
using DataAccess.Repositories.IRepository;


namespace API.Controllers
{
    public class MembersController : ApiController
    {
        #region Properties

        private IMembersRepository dbMembers;

        #endregion

        public MembersController(IMembersRepository dbMembers)
        {
            this.dbMembers = dbMembers;
        }

        // GET api/Members
        [HttpGet]
        public IHttpActionResult GetMembers()
        {
            return Ok(dbMembers.FindAll());
        }

        // GET api/Members/5
        [HttpGet]
        public IHttpActionResult GetMembers(int id)
        {
            Members member = dbMembers.FindByID(id);

            if (member == null)
                return BadRequest();

            return Ok(member);
        }

        // POST api/Members
        [HttpPost]
        public IHttpActionResult Post([FromBody]Members member)
        {
            throw new NotImplementedException();
        }
        
    }
}
