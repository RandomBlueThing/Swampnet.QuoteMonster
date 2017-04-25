using QuoteMonster.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteMonster.Services
{
	public interface IUserManagementService
	{
		User Find(string lookup);
	}


	public class UserManagementService : IUserManagementService
	{
		private readonly QuoteContext _context;

		public UserManagementService(QuoteContext context)
		{
			_context = context;
		}


		public User Find(string lookup)
		{
			var repo = new UserRepository(_context);
			return repo.Search(lookup);
		}
	}
}
