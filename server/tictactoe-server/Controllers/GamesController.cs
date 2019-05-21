using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tictactoe_server.Models;

namespace tictactoe_server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GamesController : Controller
	{
		private readonly TicTacToeContext _context;

		public GamesController(TicTacToeContext context)
		{
			_context = context;
		}

		// GET: api/Games
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Game>>> GetGame()
		{
			return await _context.Games.ToListAsync();
		}

		// GET: api/Games/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Game>> GetGame(int id)
		{
			var game = await _context.Games.FindAsync(id);

			if (game == null)
			{
				return NotFound();
			}

			return new ObjectResult(game);
		}

		// PUT: api/Games/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutGame(int id, Game game)
		{
			if (id != game.Id)
			{
				return BadRequest();
			}

			_context.Entry(game).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!GameExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Games
		[HttpPost]
		public async Task<ActionResult<Game>> PostGame(string marker)
		{
			Game game = new Game
			{
				PlayerOne = new Player { IsActive = marker == "X", Marker = "X" },
				PlayerTwo = new Player { IsActive = marker == "O", Marker = "O" }
			};		

			_context.Games.Add(game);
			await _context.SaveChangesAsync();

			game.Positions = new List<Position>();
			for (var i = 0; i < 9; i++)
			{
				game.Positions.Add(new Position() { Index = i, Marker = string.Empty });
			}
			await _context.SaveChangesAsync();
			return new JsonResult(game);
		}

		// DELETE: api/Games/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<Game>> DeleteGame(int id)
		{
			var game = await _context.Games.FindAsync(id);
			if (game == null)
			{
				return NotFound();
			}

			_context.Games.Remove(game);
			await _context.SaveChangesAsync();

			return game;
		}

		private bool GameExists(int id)
		{
			return _context.Games.Any(e => e.Id == id);
		}
	}
}
