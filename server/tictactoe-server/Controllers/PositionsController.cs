using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
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
		//private readonly IHubContext<NotificationHub> _hubContext;
		//public PositionsController(TicTacToeContext context, IHubContext<NotificationHub> hubContext)
		//{
		//	_context = context;
		//	_hubContext = hubContext;
		//}
		public PositionsController(TicTacToeContext context)
		{
			_context = context;	
		}

		// POST: api/Positions
		[HttpPost]
		public async Task<ActionResult<Position>> PostPosition([FromBody] Position position)
		{
			var game = _context.Games.Include(p => p.Positions).FirstOrDefault(g => g.Id == position.GameId);
			game.PlacePlayerMarker(position.Marker, position.Index);
			await _context.SaveChangesAsync();			
		
			if (game.CheckWinner())		
				return new JsonResult(new { message = position.Marker + ", You have won" , position });
		

			if (game.CheckGameOverDraw())	
				return new JsonResult(new { message = "Game is a Draw" });
		

			int aiChoice = game.GenerateAIChoice();
			if (aiChoice != 0)
			{
				var aiMarker = position.Marker == "X" ? "O" : "X";			
				game.PlacePlayerMarker(aiMarker, aiChoice);
				await _context.SaveChangesAsync();
			
				if (game.CheckWinner())	
					return new JsonResult(new { message = position.Marker + ", You have lost" });	

		
				if (game.CheckGameOverDraw())		
					return new JsonResult(new { message = "Game is a Draw" });

				//await _hubContext.Clients.All.SendAsync("Move", $"Marker: {aiMarker}, Position: {aiChoice}");

				return new JsonResult(new Position {Marker = aiMarker, Index = aiChoice, GameId = position.GameId });
				
			}

			return new JsonResult(new { message = "Game is a Draw" });

		}	
	}
}
