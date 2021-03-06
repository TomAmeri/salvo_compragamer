using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Salvo.Models
{
    public class GamePlayerDTO
    {
        public long Id { get; set; }
        public DateTime? JoinDate { get; set; }

        //public long GameId { get; set; }
        //public Game Game { get; set; }

        //public long PlayerId { get; set; }
        public PlayerDTO Player { get; set; }

        public double? Point { get; set; }
    }
}
