using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QuoteMonster.Model
{
    public class QuoteRepository
    {
		private readonly Random _rnd = new Random();

		private static readonly Quote[] _mocked = new Quote[] {
			new Quote(){Id = 1, Text = "Quote 1"},
			new Quote(){Id = 2, Text = "Quote 2"},
			new Quote(){Id = 3, Text = "Quote 3"},
			new Quote(){Id = 4, Text = "Quote 4"},
			new Quote(){Id = 5, Text = "Quote 5"},
			new Quote(){Id = 6, Text = "Quote 6"},
			new Quote(){Id = 7, Text = "Quote 7"},
			new Quote(){Id = 8, Text = "Quote 8"},
			new Quote(){Id = 9, Text = "Quote 9"},
			new Quote(){Id = 10, Text = "Quote 10"},
		};

		public Quote Search(int id)
		{
			return _mocked.Single(x => x.Id == id);
		}



		public IEnumerable<Quote> Search()
		{
			Thread.Sleep(1000);

			return _mocked;
		}

		internal Quote GetRandom()
		{
			return _mocked[_rnd.Next(0, _mocked.Length - 1)];
		}
	}
}
