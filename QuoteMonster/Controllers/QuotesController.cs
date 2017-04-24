using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QuoteMonster.Model;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using QuoteMonster.Services;

namespace QuoteMonster.Controllers
{
    [Produces("application/json")]
    [Route("api/Quotes")]
    public class QuotesController : Controller
    {
		private readonly QuoteContext _context;
		private readonly IUserManagementService _userManagement;

		public QuotesController(IUserManagementService userManagement, QuoteContext context)
		{
			_context = context;
			_userManagement = userManagement;
		}


		[Authorize]
		[HttpGet]
		[Route("/api/Quotes")]
		public IEnumerable<Quote> Get()
		{
			var user = _userManagement.FindOrCreate(User.FindFirstValue(ClaimTypes.NameIdentifier));

			// Various claim related r&d
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);    // auth0|58d2dd5dea3bc32c8afcae90
																			// google-oauth2|112159035990137543958
			var name_claim = User.FindFirstValue(ClaimTypes.Name);          // null
			var surname_claim = User.FindFirstValue(ClaimTypes.Surname);    // null
			var email_claim = User.FindFirstValue(ClaimTypes.Email);        // null
			var x = User.Identity.Name;                                     // null
			var y = User.Identity.AuthenticationType;                       // AuthenticationTypes.Federation
			var claims = User.Claims;


			var repo = new QuoteRepository(_context);

			return repo.Search();
		}

		[Authorize]
		[HttpGet]
		[Route("/api/Quotes/{id}")]
		public Quote Get(int id)
		{
			var user = _userManagement.FindOrCreate(User.FindFirstValue(ClaimTypes.NameIdentifier));

			var repo = new QuoteRepository(_context);

			return id == 0
				? new Quote()
				: repo.Search(id);
		}


		[Authorize]
		[HttpPost]
		public Quote Save([FromBody] Quote quote)
		{
			var user = _userManagement.FindOrCreate(User.FindFirstValue(ClaimTypes.NameIdentifier));

			var repo = new QuoteRepository(_context);

			repo.Save(quote, user);

			return quote;
		}


		[HttpGet]
		[Route("/api/Quotes/Random")]
		public Quote GetRandom()
		{
			var repo = new QuoteRepository(_context);

			return repo.GetRandom();
		}
	}
}