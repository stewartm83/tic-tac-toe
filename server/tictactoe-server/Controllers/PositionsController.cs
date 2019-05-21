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
	public class PositionsController : ControllerBase
	{
		private readonly TicTacToeContext _context;

		public PositionsController(TicTacToeContext context)
		{
			_context = context;
		}

		// GET: api/Positions
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Position>>> GetPosition()
		{
			return await _context.Positions.ToListAsync();
		}

		// GET: api/Positions/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Position>> GetPosition(int id)
		{
			var position = await _context.Positions.FindAsync(id);

			if (position == null)
			{
				return NotFound();
			}

			return position;
		}

		// PUT: api/Positions/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutPosition(int id, Position position)
		{
			if (id != position.Id)
			{
				return BadRequest();
			}

			_context.Entry(position).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!PositionExists(id))
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

		// POST: api/Positions
		[HttpPost]
		public async Task<ActionResult<Position>> PostPosition([FromBody] Position position)
		{
			var game = _context.Games.Include(p => p.Positions).First(g => g.Id == position.GameId);
			game.PlacePlayerMarker(position.Marker, position.Index);
			await _context.SaveChangesAsync();

			
			bool isWinner = game.CheckWinner();
			if (isWinner)
			{
				return new JsonResult(new { message = position.Marker + ", You have won" });
			}

			bool isDraw = game.CheckGameOverDraw();
			if (isDraw)
			{
				return new JsonResult(new { message = "Game is a Draw" });
			}

			int aiChoice = game.GenerateAIChoice();
			if (aiChoice != 0)
			{
				var aiMarker = position.Marker == "X" ? "O" : "X";			
				game.PlacePlayerMarker(aiMarker, aiChoice);
				await _context.SaveChangesAsync();

				isWinner = game.CheckWinner();
				if (isWinner)
				{
					return new JsonResult(new { message = position.Marker + ", You have lost" });
				}

				isDraw = game.CheckGameOverDraw();
				if (isDraw)
				{
					return new JsonResult(new { message = "Game is a Draw" });
				}
			

				return new JsonResult(new Position {Marker = aiMarker, Index = aiChoice, GameId = position.GameId });
			}

			return new JsonResult(new { message = "Game is a Draw" });

		}

		// DELETE: api/Positions/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<Position>> DeletePosition(int id)
		{
			var position = await _context.Positions.FindAsync(id);
			if (position == null)
			{
				return NotFound();
			}

			_context.Positions.Remove(position);
			await _context.SaveChangesAsync();

			return position;
		}

		private bool PositionExists(int id)
		{
			return _context.Positions.Any(e => e.Id == id);
		}
	}
}
