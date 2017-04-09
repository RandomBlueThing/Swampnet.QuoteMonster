using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace QuoteMonster.Model
{
	public class PropertyContext : DbContext
    {
		public PropertyContext(DbContextOptions<PropertyContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Property>()
				.ToTable("Property");

			base.OnModelCreating(modelBuilder);
		}


		public DbSet<Property> Properties { get; set; }
	}
}
