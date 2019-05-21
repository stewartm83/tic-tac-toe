using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tictactoe_server.Models
{
	public class Player
	{
		public int Id { get; set; }
		public string Marker { get; set; }

		public bool IsActive { get; set; }
	}
}
