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


		public IEnumerable<Quote> Search(User user, string text, string author, int? page, int? pageSize)
		{
			var quotes = _context.Quotes
				.Include(q => q.CreatedBy)
				.Include(q => q.Author)
				.AsQueryable();

			quotes = quotes.Where(q => q.IsEnabled);
			
			if (!string.IsNullOrEmpty(text))
			{
				quotes = quotes.Where(q => q.Text.Contains(text));
			}

			if (!string.IsNullOrEmpty(author))
			{
				quotes = quotes.Where(q => q.Author.Name.Contains(author));
			}

			if(page.HasValue && pageSize.HasValue)
			{
				quotes = quotes.Skip(page.Value * pageSize.Value).Take(pageSize.Value);
			}

			var result = quotes.OrderBy(q => q.Text).ToArray();

			return result;
		}


		public Quote GetRandom()
		{
			return _context.Quotes.Include(q => q.Author).Where(q => q.IsEnabled).OrderBy(q => Guid.NewGuid()).First();
		}

		public Quote CreateQuote(User user)
		{
			var quote = new Quote()
			{
				CreatedByUserId = user.Id,
				CreatedOn = DateTime.UtcNow,
				IsEnabled = true
			};

			_context.Quotes.Add(quote);
			return quote;
		}

		internal Author FindAuthor(string author)
		{
			return _context.Authors.SingleOrDefault(a => a.Name == author);
		}


		internal IEnumerable<string> AllAuthors()
		{
			return _context.Authors.Select(a => a.Name);
		}

		internal void DeleteQuote(Quote q)
		{
			q.IsEnabled = false;
		}
	}
}
