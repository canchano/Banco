using System;
using ApiAplication.Domain.DTO;
using Microsoft.EntityFrameworkCore;

namespace ApiAplication.Persistence
{
	public class LogDbContext:DbContext
	{
		public DbSet<Log> logs => Set<Log>();
		public LogDbContext(DbContextOptions<LogDbContext> option):base(option)
		{
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

