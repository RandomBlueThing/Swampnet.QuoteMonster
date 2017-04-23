using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteMonster.Model
{
    public class UserRepository
    {
		private readonly QuoteContext _context;

		public UserRepository(QuoteContext context)
		{
			_context = context;
		}

		public User Search(string lookup)
		{
			return _context.Users.SingleOrDefault(u => u.Lookup == lookup);
		}

		internal User CreateNewUser(string lookup)
		{
			var user = new User()
			{
				Id = Guid.NewGuid(),
				Lookup = lookup,
				CreatedOn = DateTime.UtcNow,
				IsActive = true
			};

			_context.Add(user);

			return user;
		}
	}
}
