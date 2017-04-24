using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace QuoteMonster.Model
{
    public class QuoteRepository
    {
		private readonly QuoteContext _context;

		public QuoteRepository(QuoteContext context)
		{
			_context = context;
		}


		public Quote Search(int id)
		{
			return _context.Quotes.Single(q => q.Id == id);
		}


		public IEnumerable<Quote> Search(User user)
		{
			var quotes = _context.Quotes.Include(q => q.CreatedBy).OrderByDescending(q => q.CreatedOn).ToArray();
			foreach(var q in quotes)
			{
				q.IsOneOfUsers = q.CreatedByUserId == user.Id;
			}
			return quotes;
		}


		public Quote GetRandom()
		{
			return _context.Quotes.OrderBy(q => Guid.NewGuid()).First();
		}


		public Quote Save(Quote quote, User user)
		{
			// Insert
			if(quote.Id == 0)
			{
				quote.CreatedOn = DateTime.UtcNow;
				quote.CreatedByUserId = user.Id;
				_context.Add(quote);
			}

			// Update
			else
			{
				_context.Update(quote);
			}

			_context.SaveChanges();

			return quote;
		}
	}
}
