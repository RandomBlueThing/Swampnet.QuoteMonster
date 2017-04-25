using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QuoteMonster.Model;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using QuoteMonster.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;
using Newtonsoft.Json;

namespace QuoteMonster.Controllers
{
	[Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        private readonly QuoteContext _context;
		private readonly IUserManagementService _userManagement;


		public UsersController(QuoteContext context, IUserManagementService userManagement)
        {
            _context = context;
			_userManagement = userManagement;
        }



		[HttpGet]
		[Authorize]
		[HttpGet("GetCurrent")]
		public User GetCurrent()
		{
			return _userManagement.Find(User.FindFirstValue(ClaimTypes.NameIdentifier));			
		}


		[Authorize]
		[Produces("application/json")]
		[HttpGet("OnLogin")]
		public async Task<IActionResult> OnLogin(string access_token)
		{
			// @TODO: Check our User record
			var nameId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var repo = new UserRepository(_context);
			var user = repo.Search(nameId);
			if (user == null)
			{
				user = await CreateNewUserAsync(nameId, access_token);
			}
			user.LastLogin = DateTime.UtcNow;

			await _context.SaveChangesAsync();

			return Ok(user);
		}


		private async Task<User> CreateNewUserAsync(string nameId, string access_token)
		{
			var repo = new UserRepository(_context);
			var user = repo.CreateNewUser(nameId);

			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
				var rs = await client.GetStringAsync("https://swampnet.eu.auth0.com/userinfo");
				Debug.WriteLine(rs);

				var x = JsonConvert.DeserializeObject<xxx>(rs);

				user.AvatarUrl = x.picture;
				user.Email = x.email;
				user.DisplayName = x.name;
			}

			return user;
		}


		// GET: api/Users
		[HttpGet]
		[Authorize]
        public IEnumerable<User> GetUsers()
        {
            return _context.Users;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
		[Authorize]
		public async Task<IActionResult> GetUser([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
		[Authorize]
		public async Task<IActionResult> PutUser([FromRoute] Guid id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
		[Authorize]
		public async Task<IActionResult> PostUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
		[Authorize]
		public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }

	class xxx
	{
		public string email { get; set; }
		public string name { get; set; }
		public string given_name { get; set; }
		public string family_name { get; set; }
		public string picture { get; set; }
		public string nickname { get; set; }
	}

}