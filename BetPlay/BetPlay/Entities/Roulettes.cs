﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetPlay.Entities
{
    public class Roulettes
    {
        public int Id { get; set; }
        public bool State { get; set; }
        public string DateOpen { get; set; }
        public string DateClose { get; set; }
    }
}
