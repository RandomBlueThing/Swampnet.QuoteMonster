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

		//[Authorize]
		[HttpGet]
		[Route("api/Search")]
		public IEnumerable<Quote> Get()
		{
			var repo = new QuoteRepository();

			return repo.Search();
		}

		//[Authorize]
		[HttpGet]
		[Route("api/Search/{id?}")]
		public Quote Get(int id)
		{
			var repo = new QuoteRepository();

			return repo.Search(id);
		}


		[HttpGet]
		[Route("api/RandomQuote")]
		public Quote GetRandom()
		{
			var repo = new QuoteRepository();

			return repo.GetRandom();
		}
	}
}