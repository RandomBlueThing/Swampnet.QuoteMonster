﻿using Microsoft.EntityFrameworkCore;
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

			modelBuilder.Entity<User>()
				.ToTable("User");

			base.OnModelCreating(modelBuilder);
		}


		public DbSet<Quote> Quotes { get; set; }
		public DbSet<User> Users { get; set; }
	}
}
