using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Salvo.Models;
using Salvo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Salvo.Controllers
{
    [Route("api/players")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private IPlayerRepository _repository;

        public PlayersController(IPlayerRepository repository)
        {
            _repository = repository;
        }
        
        [HttpPost]
        public IActionResult Post([FromBody] PlayerDTO player)
        {
            try
            {
                //si todos los datos son invalidos lo avisa
                if (String.IsNullOrEmpty(player.Email) && String.IsNullOrEmpty(player.Password) && String.IsNullOrEmpty(player.Name))
                {
                    return StatusCode(403, "Datos inválidos");
                }
                //verifica dato por dato y avisa
                if(String.IsNullOrEmpty(player.Email))
                    return StatusCode(403, "Email vacío");
                if (String.IsNullOrEmpty(player.Password))
                    return StatusCode(403, "Password vacío");
                if (String.IsNullOrEmpty(player.Name))
                    return StatusCode(403, "Name vacío");
                //regula el mail y la contra
                if((player.Password.Length <= 8) && (!player.Email.Contains("@")))
                    return StatusCode(403, "Email y password inválidos");
                if (player.Password.Length <= 8)
                    return StatusCode(403, "Password muy corta");
                if(!player.Email.Contains("@"))
                    return StatusCode(403, "Email inválido");



                Player dbPlayer = _repository.FindByEmail(player.Email);
                if (dbPlayer != null)
                {
                    return StatusCode(403, "Email está en uso");
                }

                Player newPlayer = new Player
                {
                    Email = player.Email,
                    Password = player.Password,
                    Name = player.Name
                };

                _repository.Save(newPlayer);

                return StatusCode(201, newPlayer);

            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
