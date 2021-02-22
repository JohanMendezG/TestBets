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
        public int Number { get; set; }
        [RegularExpression("^[0-1]{1}$", ErrorMessage ="Debe elegir Rojo: 1, Negro: 0")]
        public int Color { get; set; }
        public bool BetNumber { get; set; }
        public decimal Money { get; set; }
        [Required]
        public int RouletteId { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
