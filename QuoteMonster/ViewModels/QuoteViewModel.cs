using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteMonster.ViewModels
{
    public class QuoteViewModel
    {
		public int Id { get; set; }
		public string Text { get; set; }
		public string Author { get; set; }
		public bool CanEdit { get; set; }
	}
}
