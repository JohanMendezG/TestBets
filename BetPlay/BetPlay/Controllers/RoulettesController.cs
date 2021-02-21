using BetPlay.Data.Roulettes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetPlay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoulettesController : ControllerBase
    {
        private readonly IRoulettes roulettes;
        public RoulettesController(IRoulettes roulettes)
        {
            this.roulettes = roulettes;
        }
        [HttpGet("{id}")]
        public 

    }
}
