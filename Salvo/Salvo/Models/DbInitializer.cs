using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Salvo.Models
{
    public static class DbInitializer
    {
        public static void Initialize(SalvoContext context)
        {
            
            if (!context.Players.Any())
            {
                var players = new Player[]
                {
                    new Player{ 
                        Email="j.bauer@ctu.gov",
                        Name="Jack Bauer",
                        Password="24" 
                    },                   
                     new Player{
                        Email="c.obrian@ctu.gov",
                        Name="Chloe O'Brian",
                        Password="42"
                    },
                     new Player{
                        Email="kim_bauer@gmail.com",
                        Name="Kim Bauer",
                        Password="kb"
                    },
                       new Player{
                        Email="t.almeida@ctu.gov",
                        Name="Tony Almeida",
                        Password="mole"
                    },
                };
                //recorre los players
                foreach(Player p in players)
                {
                    context.Players.Add(p);
                }
                //guarda cambio 
                context.SaveChanges();
            }

            if (!context.Games.Any())
            {
                var games = new Game[]
                {
                    new Game {CreationTime = DateTime.Now},
                    new Game {CreationTime = DateTime.Now.AddHours(1)},
                    new Game {CreationTime = DateTime.Now.AddHours(2)},
                    new Game {CreationTime = DateTime.Now.AddHours(3)},
                    new Game {CreationTime = DateTime.Now.AddHours(4)},
                    new Game {CreationTime = DateTime.Now.AddHours(5)},
                    new Game {CreationTime = DateTime.Now.AddHours(6)},
                    new Game {CreationTime = DateTime.Now.AddHours(7)},
                    new Game {CreationTime = DateTime.Now.AddHours(8)},
                };
                //recorre los game
                foreach (Game g in games)
                {
                    context.Games.Add(g);
                }
                //guarda cambio 
                context.SaveChanges();
            }

            if (!context.GamePlayers.Any())
            {
                var gamePlayers = new GamePlayer[]
                {
                    new GamePlayer
                    {
                        JoinDate = context.Games.Find(1L).CreationTime,
                        GameId =  1,
                        Game = context.Games.Find(1L),
                        PlayerId = 1,
                        Player = context.Players.Find(1L)

                    },
                    new GamePlayer
                    {
                        JoinDate = context.Games.Find(1L).CreationTime,
                        GameId =  1,
                        Game = context.Games.Find(1L),
                        PlayerId = 2,
                        Player = context.Players.Find(2L)

                    },
                    new GamePlayer
                    {
                        JoinDate = context.Games.Find(2L).CreationTime,
                        GameId =  2,
                        Game = context.Games.Find(2L),
                        PlayerId = 1,
                        Player = context.Players.Find(1L)

                    },
                    new GamePlayer
                    {
                        JoinDate = context.Games.Find(2L).CreationTime,
                        GameId =  2,
                        Game = context.Games.Find(2L),
                        PlayerId = 2,
                        Player = context.Players.Find(2L)

                    },
                    new GamePlayer
                    {
                        JoinDate = context.Games.Find(3L).CreationTime,
                        GameId =  3,
                        Game = context.Games.Find(3L),
                        PlayerId = 2,
                        Player = context.Players.Find(2L)

                    },
                    new GamePlayer
                    {
                        JoinDate = context.Games.Find(3L).CreationTime,
                        GameId =  3,
                        Game = context.Games.Find(3L),
                        PlayerId = 4,
                        Player = context.Players.Find(4L)

                    },
                    new GamePlayer
                    {
                        JoinDate = context.Games.Find(4L).CreationTime,
                        GameId =  4,
                        Game = context.Games.Find(4L),
                        PlayerId = 1,
                        Player = context.Players.Find(1L)

                    },
                    new GamePlayer
                    {
                        JoinDate = context.Games.Find(4L).CreationTime,
                        GameId =  4,
                        Game = context.Games.Find(4L),
                        PlayerId = 2,
                        Player = context.Players.Find(2L)

                    },
                    new GamePlayer
                    {
                        JoinDate = context.Games.Find(5L).CreationTime,
                        GameId =  5,
                        Game = context.Games.Find(5L),
                        PlayerId = 4,
                        Player = context.Players.Find(4L)

                    },
                    new GamePlayer
                    {
                        JoinDate = context.Games.Find(5L).CreationTime,
                        GameId =  5,
                        Game = context.Games.Find(5L),
                        PlayerId = 1,
                        Player = context.Players.Find(1L)

                    }

                };
                foreach (GamePlayer gp in gamePlayers)
                {
                    context.GamePlayers.Add(gp);
                }
                //guarda cambio 
                context.SaveChanges();

            }
        }
    }
}
