using Server.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace server_HW3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            User user = new User();
            return user.Read();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsersController>
        [HttpPost]
        public bool Post([FromBody] User user)
        {
            return user.Insert();
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public bool Put(string id, [FromBody] User user)
        {
            user.Email = id;

            if (user.Update() != 0)
            {
                return true;
            }
            else
                return false;
        }

        // GET(LOGIN) api/<UsersController>/5
        [HttpGet("/Login")]
        public User UserLogin(string email , string password)
        {
            User user = new User();
            return user.Login(email, password);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public bool Delete(string id)
        {
            User user = new User();
            user.Email = id;
            return user.Delete ();
        }
    }
}
