using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tictactoe_server.Models
{
	public class Position
	{
		public int Id { get; set; }
		public int Index { get; set; }

		public int GameId { get; set; }
		public string Marker { get; set; }
		public Game Game { get; set; }
	}
}
