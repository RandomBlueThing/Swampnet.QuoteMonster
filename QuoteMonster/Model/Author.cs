using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteMonster.Model
{
    public class Author
    {
		public Author()
		{
			CreatedOn = DateTime.UtcNow;
		}

		public int Id { get; set; }

		public string Name { get; set; }

		public DateTime CreatedOn { get; set; }

		[Newtonsoft.Json.JsonIgnore]
		public List<Quote> Quotes { get; set; }
	}
}
