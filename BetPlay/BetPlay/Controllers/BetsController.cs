using BetPlay.Data.Bets;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BetPlay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BetsController : ControllerBase
    {
        private readonly IBets bets;
        public BetsController(IBets bets)
        {
            this.bets = bets;
        }
        [HttpPost]
        public ActionResult<string> LoadBet([FromBody] Entities.Bets bet, [FromHeader] int userId)
        {
            try
            {
                if (ValidateBetParameters(bet))
                {
                    bet.UserId = userId;
                    bets.LoadBet(bet);
                    return "Apuesta almacenada";
                }
                return "La apuesta no se completo";
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.Contains("inner") ? ex.InnerException.Message : ex.Message);
            }
        }
        private bool ValidateBetParameters(Entities.Bets bet)
        {
            if (!RouletteIsOpen(bet.RouletteId))
                throw new Exception("La ruleta esta cerrada");
            if (!BetMoneyMax(bet))
                throw new Exception("Dinero supera el maximo permitido");
            if (!BetNumberOk(bet.Number))
                throw new Exception("El numero debe esta entre 0 y 36");
            return true;
        }
        private bool RouletteIsOpen(int RouletteId)
        {
            return bets.ValidationRouletteIsOpen(RouletteId);
        }
        private bool BetMoneyMax(Entities.Bets bet)
        {
            return ((bet.Money + bets.MoneyByRoulette(bet.RouletteId)) <= 10000);
        }
        private bool BetNumberOk(int number)
        {
            return (number >= 0) && (number <= 36);
        }
    }
}
