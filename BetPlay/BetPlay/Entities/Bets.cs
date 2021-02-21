using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BetPlay.Entities
{
    public class Bets
    {
        public int Id { get; set; }
        public int? Number { get; set; }
        public int? Color { get; set; }
        public decimal Money { get; set; }
        public int RouletteId { get; set; }
        public int UserId { get; set; }
    }
}
