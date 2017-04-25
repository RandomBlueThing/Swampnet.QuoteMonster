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
		public DateTime? LastLogin { get; set; }
		public string DisplayName { get; set; }
		public string Email { get; set; }
		public string AvatarUrl { get; set; }
		public bool IsActive { get; set; }
		
		#region VM-Ish
		public bool IsNew { get; set; }
		#endregion

		public List<Quote> Quotes { get; set; }
	}
}
