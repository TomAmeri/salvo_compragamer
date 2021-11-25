using Microsoft.AspNetCore.Authorization;
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
    [Authorize("PlayerOnly")]
    public class GamePlayersController : ControllerBase
    {
        private IGamePlayerRepository _repository;
        private IPlayerRepository _playerRepository;
       public GamePlayersController(IGamePlayerRepository repository, IPlayerRepository playerRepository)
        {
            _repository = repository;
            _playerRepository = playerRepository;
        }

        // GET api/<GamePlayersController>/5
        [HttpGet("{id}", Name = "GetGameView")]
        public IActionResult GetGameView(long id)
        {
            try
            {
                string email = User.FindFirst("Player") != null ? User.FindFirst("Player").Value : "Guest";

                var gp = _repository.GetGamePlayerView(id);

                if (gp.Player.Email != email)
                    return Forbid();

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

        [HttpPost("{id}/ships")]
        public IActionResult Post(long id, [FromBody] List<ShipDTO> ships)
        {
            try
            {
                string email = User.FindFirst("Player") != null ? User.FindFirst("Player").Value : "Guest";
                Player player = _playerRepository.FindByEmail(email);
                GamePlayer gamePlayer = _repository.FindById(id);

                if (gamePlayer == null)
                    return StatusCode(403, "No existe el juego");

                if (gamePlayer.Player.Id != player.Id)
                    return StatusCode(403, "El usuario no se encuentra en el juego");

                if (gamePlayer.Ships.Count == 5)
                    return StatusCode(403, "Ya se han posicionado los barcos");

                gamePlayer.Ships = ships.Select(ship => new Ship
                {
                    GamePlayerId = gamePlayer.Id,
                    Type = ship.Type,
                    Locations = ship.Locations.Select(location => new ShipLocation
                    {
                        ShipId = ship.Id,
                        Location = location.Location,
                    }).ToList(),
                }).ToList();

                _repository.Save(gamePlayer);

                return StatusCode(201);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
