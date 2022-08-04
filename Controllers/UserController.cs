using CityInYourPocket.Interface;
using CityInYourPocket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CityInYourPocket.Controllers
{
    [Authorize]
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUser _iuser;

        public UserController(IUser iuser)
        {
            _iuser = iuser;
        }

        //get all users
        // GET: api/user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await Task.FromResult(_iuser.GetUsers());
        }

        //add user
        // POST: api/user
        [HttpPost]
        public async Task<ActionResult<User>> Post(User user)
        {
            _iuser.AddUser(user);
            return await Task.FromResult(user);
        }

        //update user data
        // PUT api/user/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Put(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            try
            {
                _iuser.UpdateUser(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_iuser.CheckUser(id))
                {
                    return NotFound("Couldn't find user");
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(user);
        }

        //delete user
        // DELETE api/user/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(int id)
        {
            var result = _iuser.DeleteUser(id);
            return await Task.FromResult(result);
        }

        //get user by id
        // GET: api/user/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var usr = await Task.FromResult(_iuser.GetUserById(id));
            return usr;
        }
    }
}
