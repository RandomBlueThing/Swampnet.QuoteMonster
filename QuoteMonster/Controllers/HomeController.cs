using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using QuoteMonster.Model;
using QuoteMonster.Services;
using System.Security.Claims;
using System;
using Newtonsoft.Json;

namespace QuoteMonster.Controllers
{
	public class HomeController : Controller
    {
		private readonly IUserManagementService _userManagement;
		private readonly QuoteContext _context;

		public HomeController(IUserManagementService userManagement, QuoteContext context)
		{
			_userManagement = userManagement;
			_context = context;
		}

        public IActionResult Index()
        {
            return View();
        }

	}
}