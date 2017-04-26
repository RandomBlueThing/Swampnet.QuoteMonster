using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteMonster.Model
{
    public class QuoteContext : DbContext
	{
		public QuoteContext(DbContextOptions<QuoteContext> options)
			: base(options)
		{
		}


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Quote>()
				.ToTable("Quote");

			modelBuilder.Entity<Quote>()
				.HasOne(q => q.CreatedBy)
				.WithMany(u => u.Quotes)
				.HasForeignKey(q => q.CreatedByUserId);

			modelBuilder.Entity<Quote>()
				.Ignore(q => q.CanEdit);

			modelBuilder.Entity<User>()
				.ToTable("User");

			modelBuilder.Entity<User>().Ignore(u => u.IsNew);

			modelBuilder.Entity<Author>()
				.ToTable("Author");

			base.OnModelCreating(modelBuilder);
		}


		public DbSet<Quote> Quotes { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Author> Authors { get; set; }
	}
}
