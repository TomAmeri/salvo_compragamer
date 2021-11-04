using Microsoft.EntityFrameworkCore;
using Salvo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Salvo.Repositories
{
    public class GameRepository : RepositoryBase<Game>, IGameRepository
    {
        public GameRepository(SalvoContext repositoryContext) : base(repositoryContext)
        {

        }
        public IEnumerable<Game> GetAllGames()
        {
            return FindAll()
                 .OrderBy(game => game.CreationTime)
                 .ToList();
        }
        public IEnumerable<GameDTO> GetAllGamesWithPlayers()
        {
            return FindAll(source => source.Include(game => game.GamePlayers)
                        .ThenInclude(gameplayer => gameplayer.Player))
                    .OrderBy(game => game.CreationTime)
                        .Select(g => new GameDTO
                        {
                            Id = g.Id,
                            CreationTime = g.CreationTime,
                            GamePlayers = g.GamePlayers.Select(gp => new GamePlayerDTO { Id = gp.Id, JoinDate = gp.JoinDate, Player = new PlayerDTO { Id = gp.Player.Id, Email = gp.Player.Email } }).ToList()
                        })
                    .ToList();
        }

    }
}
