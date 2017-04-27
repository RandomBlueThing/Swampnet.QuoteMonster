using QuoteMonster.Model;
using QuoteMonster.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteMonster
{
    static class QuoteExtensions
    {
		public static QuoteViewModel ToViewModel(this Quote quote, User user = null)
		{
			return new QuoteViewModel()
			{
				Id = quote.Id,
				Author = quote.Author?.Name,
				Text = quote.Text,
				CanEdit = user != null && user.Id == quote.CreatedByUserId
			};
		}
    }
}
