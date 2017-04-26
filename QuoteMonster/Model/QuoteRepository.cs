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
			return _context.Quotes
				.Include(q => q.CreatedBy)
				.Include(q => q.Author)
				.Single(q => q.Id == id);
		}


		public IEnumerable<Quote> Search(User user, string text, string author)
		{
			IEnumerable<Quote> quotes = _context.Quotes
				.Include(q => q.CreatedBy)
				.Include(q => q.Author)
				;
			
			if (!string.IsNullOrEmpty(text))
			{
				quotes = quotes.Where(q => q.Text.Contains(text));
			}

			if (!string.IsNullOrEmpty(author))
			{
				quotes = quotes.Where(q => q.Author.Name.Contains(author));
			}

			var result = quotes.OrderByDescending(q => q.CreatedOn).ToArray();

			if (user != null)
			{
				foreach (var q in result)
				{
					q.CanEdit = q.CreatedByUserId == user.Id;
				}
			}
			return result;
		}


		public Quote GetRandom()
		{
			return _context.Quotes.Include(q => q.Author).OrderBy(q => Guid.NewGuid()).First();
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

			return quote;
		}
	}
}
