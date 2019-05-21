using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tictactoe_server.Models
{
	public class Game
	{
		public int Id { get; set; }
		public Player PlayerOne { get; set; }
		public Player PlayerTwo { get; set; }

		public ICollection<Position> Positions { get; set; }

	}
}
