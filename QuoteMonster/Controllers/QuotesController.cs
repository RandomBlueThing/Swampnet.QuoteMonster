using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QuoteMonster.Model;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using QuoteMonster.Services;
using QuoteMonster.ViewModels;
using System;
using System.Threading.Tasks;

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


		[HttpGet]
		[Route("/api/Quotes")]
		public IEnumerable<QuoteViewModel> Get([FromQuery] string text, [FromQuery] string author, [FromQuery] int? page, [FromQuery] int? pageSize)
		{
			var user = _userManagement.Find(User.FindFirstValue(ClaimTypes.NameIdentifier));

			// Various claim related r&d
			//var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);    // auth0|58d2dd5dea3bc32c8afcae90
			//																// google-oauth2|112159035990137543958
			//var name_claim = User.FindFirstValue(ClaimTypes.Name);          // null
			//var surname_claim = User.FindFirstValue(ClaimTypes.Surname);    // null
			//var email_claim = User.FindFirstValue(ClaimTypes.Email);        // null
			//var x = User.Identity.Name;                                     // null
			//var y = User.Identity.AuthenticationType;                       // AuthenticationTypes.Federation
			//var claims = User.Claims;


			var repo = new QuoteRepository(_context);
			var quotes = repo.Search(user, text, author, page, pageSize);

			return quotes.Select(q => q.ToViewModel(user));
		}

		[HttpGet]
		[Route("/api/Authors")]
		public IEnumerable<string> GetAuthors()
		{
			return new QuoteRepository(_context)
				.AllAuthors()
				.ToArray();
		}

		[Authorize]
		[HttpGet]
		[Route("/api/Quotes/{id}")]
		public QuoteViewModel Get(int id)
		{
			var user = _userManagement.Find(User.FindFirstValue(ClaimTypes.NameIdentifier));

			var repo = new QuoteRepository(_context);

			return id == 0
				? new QuoteViewModel()
				: repo.Search(id).ToViewModel(user);
		}


		[Authorize]
		[HttpPost]
		public IActionResult Save([FromBody] QuoteViewModel quote)
		{
			var user = _userManagement.Find(User.FindFirstValue(ClaimTypes.NameIdentifier));

			var repo = new QuoteRepository(_context);
			Quote q;

			// New Quote
			if(quote.Id == 0)
			{
				q = repo.CreateQuote(user);
			}
			// Update existing
			else
			{
				q = repo.Search(quote.Id);
				if(q == null)
				{
					// return 404?
					return NotFound();
				}
				else
				{
					if(q.CreatedByUserId != user.Id)
					{
						// Not created by this user??
						return Unauthorized();
					}
				}
			}

			q.Text = quote.Text;

			// Check if Author has changed
			if (q.Author == null || q.Author.Name != quote.Author)
			{
				q.Author = repo.FindAuthor(quote.Author);
				if(q.Author == null)
				{
					q.Author = new Author()
					{
						Name = quote.Author,
					};
				}
			}

			_context.SaveChanges();

			return Ok(quote);
		}


		[HttpGet]
		[Route("/api/Quotes/Random")]
		public QuoteViewModel GetRandom()
		{
			var repo = new QuoteRepository(_context);

			return repo.GetRandom().ToViewModel();
		}


		// DELETE: api/Quotes/5
		[HttpDelete("{id}")]
		[Authorize]
		public async Task<IActionResult> DeleteQuote([FromRoute] int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var user = _userManagement.Find(User.FindFirstValue(ClaimTypes.NameIdentifier));

			var repo = new QuoteRepository(_context);
			var q = repo.Search(id);
			if (q == null)
			{
				// return 404?
				return NotFound();
			}
			else
			{
				if (q.CreatedByUserId != user.Id)
				{
					// Not created by this user??
					return Unauthorized();
				}
			}

			repo.DeleteQuote(q);

			await _context.SaveChangesAsync();

			return Ok(user);
		}
	}
}