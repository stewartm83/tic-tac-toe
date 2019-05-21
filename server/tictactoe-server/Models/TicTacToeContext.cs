using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tictactoe_server.Models;

namespace tictactoe_server.Models
{
	public class TicTacToeContext : DbContext
	{

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Data Source=tictactoe.db");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Game>()
				.HasMany(c => c.Positions)
				.WithOne(e => e.Game);
		}

		public DbSet<Game> Games { get; set; }

		public DbSet<Position> Positions { get; set; }

		public DbSet<Player> Players { get; set; }
	}
}
