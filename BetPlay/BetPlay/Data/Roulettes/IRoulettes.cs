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
        bool AddRoulette(BetPlay.Entities.Roulettes roulette);
        bool EditRoulette(BetPlay.Entities.Roulettes roulette);
    }
}
