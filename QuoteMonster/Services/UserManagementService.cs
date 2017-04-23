using QuoteMonster.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteMonster.Services
{
	public interface IUserManagementService
	{
		User FindOrCreate(string lookup);
	}


	public class UserManagementService : IUserManagementService
	{
		private readonly QuoteContext _context;

		public UserManagementService(QuoteContext context)
		{
			_context = context;
		}


		public User FindOrCreate(string lookup)
		{
			var repo = new UserRepository(_context);
			var user = repo.Search(lookup);
			if(user == null)
			{
				user = repo.CreateNewUser(lookup);

				_context.SaveChanges();
			}
			return user;
		}
	}
}
