using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetPlay.Data.Roulettes
{
    public interface IRoulettes
    {
        BetPlay.Entities.Roulettes GetRoulette(int id);
        List<BetPlay.Entities.Roulettes> GetRoulettes();
        int CreateRoulette();
        bool OpenRoulette(int id);
        bool CloseRoulette(int id);
        List<Entities.Bets> BetsResults(int roulettesId);
    }
}
