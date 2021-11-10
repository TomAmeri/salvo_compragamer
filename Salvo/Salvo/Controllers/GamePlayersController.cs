using Microsoft.AspNetCore.Mvc;
using Salvo.Models;
using Salvo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Salvo.Controllers
{
    [Route("api/gamePlayers")]
    [ApiController]
    public class GamePlayersController : ControllerBase
    {
        private IGamePlayerRepository _repository;

        public GamePlayersController(IGamePlayerRepository repository)
        {
            _repository = repository;
        }

        // GET api/<GamePlayersController>/5
        [HttpGet("{id}", Name = "GetGameView")]
        public IActionResult GetGameView(long id)
        {
            try
            {
                var gp = _repository.GetGamePlayerView(id);
                var gameviewdto = new GameViewDTO
                {
                    Id = gp.Id,
                    CreationDate = gp.Game.CreationDate,
                    Ships = gp.Ships
                                        .Select(sh => new ShipDTO
                                        {
                                            Id = sh.Id,
                                            Type = sh.Type,
                                            Locations = sh.Locations
                                            .Select(loc => new ShipLocationDTO
                                            {
                                                Id = loc.Id,
                                                Location = loc.Location
                                            }).ToList()
                                        }
                                        ).ToList(),
                    GamePlayers = gp.Game.GamePlayers
                                 .Select(gps => new GamePlayerDTO
                                 {
                                     Id = gps.Id,
                                     JoinDate = gps.JoinDate,
                                     Player = new PlayerDTO
                                     {
                                         Id = gps.Player.Id,
                                         Email = gps.Player.Email
                                     }
                                 }).ToList(),
                    Salvos = gp.Game.GamePlayers.SelectMany(gps => gps.Salvos.Select(salvo => new SalvoDTO
                    {
                        Id = salvo.Id,
                        Turn = salvo.Turn,
                        Player = new PlayerDTO
                        {
                            Id = gps.Player.Id,
                            Email = gps.Player.Email
                        },
                        Locations = salvo.Locations.Select(salvoLocation => new SalvoLocationDTO
                        {
                            Id = salvoLocation.Id,
                            Location = salvoLocation.Location
                        }).ToList()
                    })).ToList()
                };
                //return Ok();
                return Ok(gameviewdto);
            
                
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }        
    }
}
