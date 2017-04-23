using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuoteMonster.Model;
using Microsoft.AspNetCore.Authorization;

namespace QuoteMonster.Controllers
{
    [Produces("application/json")]
    public class SearchController : Controller
    {
		private readonly QuoteContext _context;

		public SearchController(QuoteContext context)
		{
			_context = context;
		}

		//[Authorize]
		[HttpGet]
		[Route("api/Search")]
		public IEnumerable<Quote> Get()
		{
			var repo = new QuoteRepository(_context);

			return repo.Search();
		}

		//[Authorize]
		[HttpGet]
		[Route("api/Search/{id?}")]
		public Quote Get(int id)
		{
			var repo = new QuoteRepository(_context);

			return id == 0 
				? new Quote()
				: repo.Search(id);
		}


		//[Authorize]
		[HttpPost]
		[Route("api/Save")]
		public Quote Post([FromBody] Quote quote)
		{
			var repo = new QuoteRepository(_context);

			repo.Save(quote);

			return quote;
		}

		[HttpGet]
		[Route("api/RandomQuote")]
		public Quote GetRandom()
		{
			var repo = new QuoteRepository(_context);

			return repo.GetRandom();
		}
	}
}