﻿using System;
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
        }
    }
}
