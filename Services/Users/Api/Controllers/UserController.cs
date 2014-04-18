using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Web.Http;
using System.Web.Http.Description;
using Burgerama.Services.Users.Api.Models;

namespace Burgerama.Services.Users.Api.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(IEnumerable<UserModel>))]
        public IHttpActionResult GetAllUsers()
        {
            var users = new[]
            {
                new UserModel { Id = Guid.NewGuid().ToString(), Email = "dstockhammer@burgerama.co.uk" },
                new UserModel { Id = Guid.NewGuid().ToString(), Email = "zsimari@burgerama.co.uk" }
            };

            return Ok(users);
        }

        [HttpGet]
        [Route("{userId}")]
        [ResponseType(typeof(UserModel))]
        public IHttpActionResult GetUser(string userId)
        {
            Contract.Requires<ArgumentNullException>(userId != null);

            var user = new UserModel { Id = userId, Email = "dstockhammer@burgerama.co.uk" };
            return Ok(user);
        }
    }
}
