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
		public DbSet<Game> Games { get; set; }
		public DbSet<Position> Positions { get; set; }
		public DbSet<Player> Players { get; set; }

		public TicTacToeContext(DbContextOptions<TicTacToeContext> options)
	  : base(options)
		{ }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Game>()
				.HasMany(c => c.Positions)
				.WithOne(e => e.Game);
		}	
	}
}
