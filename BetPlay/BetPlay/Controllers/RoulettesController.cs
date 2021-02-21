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
        [HttpGet]
        public ActionResult<List<Entities.Roulettes>> GetRoulettes()
        {
            try
            {
                return roulettes.GetRoulettes();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.Contains("inner") ? ex.InnerException.Message : ex.Message);
            }
        }
        [HttpPost]
        public ActionResult<int> CreateRoulette()
        {
            try
            {
                return roulettes.CreateRoulette();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.Contains("inner") ? ex.InnerException.Message : ex.Message);
            }
        }
        [HttpPut("/OpenRoulette/{id}")]
        public ActionResult<string> OpenRoulette(int id)
        {
            try
            {
                Entities.Roulettes roulette = roulettes.GetRoulette(id);
                if (roulette.State == true)
                    return $"Ruleta {id} apertura DENY";
                roulettes.OpenRoulette(id);
                return $"Ruleta {id} apertura OK";
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.Contains("inner") ? ex.InnerException.Message : ex.Message);
            }
        }
        [HttpPut("/CloseRoulette/{id}")]
        public ActionResult<string> CloseRoulette(int id)
        {
            try
            {
                Entities.Roulettes roulette = roulettes.GetRoulette(id);
                if (roulette.State == false)
                    return $"Ruleta {id} cierre DENY";
                roulettes.CloseRoulette(id);
                return $"Ruleta {id} cierre OK";
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.Contains("inner") ? ex.InnerException.Message : ex.Message);
            }
        }
    }
}
