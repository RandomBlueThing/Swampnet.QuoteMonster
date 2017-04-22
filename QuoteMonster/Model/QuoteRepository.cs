using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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


		public IEnumerable<Quote> Search()
		{
			return _context.Quotes.ToArray();
		}


		public Quote GetRandom()
		{
			return _context.Quotes.OrderBy(q => Guid.NewGuid()).First();
		}


		public Quote Save(Quote quote)
		{
			// Insert
			if(quote.Id == 0)
			{
				quote.CreatedOn = DateTime.UtcNow;
				_context.Add(quote);
			}

			// Update
			else
			{
				quote.ModifiedOn = DateTime.UtcNow;
				_context.Update(quote);
			}

			_context.SaveChanges();

			return quote;
		}
	}
}
