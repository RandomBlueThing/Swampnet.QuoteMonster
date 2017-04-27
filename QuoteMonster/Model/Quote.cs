using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteMonster.Model
{
    public class Quote
    {
		public int Id { get; set; }
		public string Text { get; set; }
		public DateTime CreatedOn { get; set; }
		public Guid CreatedByUserId { get; set; }
		public User CreatedBy { get; set; }
		public int AuthorId { get; set; }
		public Author Author { get; set; }
	}
}
