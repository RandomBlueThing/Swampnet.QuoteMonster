using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteMonster.Model
{
    public class User
    {
		public Guid Id { get; set; }
		public string Lookup { get; set; }
		public DateTime CreatedOn { get; set; }
		public bool IsActive { get; set; }
	}
}
