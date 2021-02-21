using BetPlay.Data.Bets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

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
        public ActionResult<string> LoadBet(Entities.Bets bet, HttpRequest request)
        {
            try
            {
                if (ValidateBetParameters(bet))
                {
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
            if (!BetMoneyMax(bet))
                return false;
            if (!BetByColor(bet))
                bet.Number = null;
            bet.Color = null;
            if (!BetNumberOk(bet.Number ?? 0))
                return false;
            return true;
        }
        private bool BetMoneyMax(Entities.Bets bet)
        {
            return ((bet.Money + bets.MoneyByRoulette(bet.RouletteId)) <= 10000);
        }
        private bool BetByColor(Entities.Bets bet)
        {
            if (bet.Number == null)
                return true;
            return false;
        }
        private bool BetNumberOk(int number)
        {
            return (number >= 0) && (number <= 36);
        }
    }
}
