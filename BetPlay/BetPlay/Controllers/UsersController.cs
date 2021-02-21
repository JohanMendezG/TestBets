using BetPlay.Data.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetPlay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsers users;
        public UsersController(IUsers users)
        {
            this.users = users;
        }
        [HttpGet("{id}")]
        public ActionResult<Entities.Users> Getuser(int id)
        {
            try
            {
                return users.GetUser(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.Contains("inner") ? ex.InnerException.Message : ex.Message);
            }
        }
    }
}
