using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tictactoe_server.Models
{
	public class Game
	{
		private int movesLeft = 9;
		public int Id { get; set; }

		public int PlayerOneId { get; set; }
		public Player PlayerOne { get; set; }
		public int PlayerTwoId { get; set; }
		public Player PlayerTwo { get; set; }

		public ICollection<Position> Positions { get; set; }


		public bool Play(string player, int position)
		{
			PlacePlayerMarker(player, position);

			return CheckWinner();
		}

		public bool CheckGameOverDraw()
		{
			return movesLeft <= 0;
		}
		public void PlacePlayerMarker(string player, int position)
		{
			movesLeft--;
			if (position < 9 && Positions.ElementAt(position).Marker == string.Empty)
			{
				Positions.ElementAt(position).Marker = player;
			}
		}
		public bool CheckWinner()
		{
			for (int i = 0; i < 3; i++)
			{
				// Check rows
				if (Positions.ElementAt(i * 3).Marker != string.Empty && Positions.ElementAt(i * 3).Marker == Positions.ElementAt((i * 3) + 1).Marker
					&& Positions.ElementAt(i * 3).Marker == Positions.ElementAt((i * 3) + 2).Marker)
				{
					return true;
				}

				// Check columns
				if (Positions.ElementAt(i).Marker != string.Empty && Positions.ElementAt(i).Marker == Positions.ElementAt(i + 3).Marker && Positions.ElementAt(i).Marker == Positions.ElementAt(i + 6).Marker)
				{
					return true;
				}
			}

			// Check diagonals
			if ((Positions.ElementAt(0).Marker != string.Empty && Positions.ElementAt(0).Marker == Positions.ElementAt(4).Marker && Positions.ElementAt(0).Marker == Positions.ElementAt(8).Marker)
				|| (Positions.ElementAt(2).Marker != string.Empty && Positions.ElementAt(2).Marker == Positions.ElementAt(4).Marker && Positions.ElementAt(2).Marker == Positions.ElementAt(6).Marker))
			{
				return true;
			}

			return false;
		}

		public List<Position> GetAvailableMoves()
		{
			var moves = new List<Position>();
			foreach (var position in Positions)
			{
				if (position.Marker == string.Empty)
				{
					moves.Add(position);
				}
			}

			return moves;
		}

		public int GenerateAIChoice()
		{
			var availableMoves = GetAvailableMoves();
			if (availableMoves.Count == 1)
			{
				return availableMoves.FirstOrDefault().Index;
			}
			if (availableMoves.Count > 1)
			{
				Random random = new Random();
				var num = random.Next(1, availableMoves.Count);
				return availableMoves.ElementAt(num).Index;
			}
			return 0;
		}
	}
}
