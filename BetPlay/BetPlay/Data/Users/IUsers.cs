using System;
using System.Collections.Generic;

namespace BetPlay.Data.Users
{
    public interface IUsers
    {
        BetPlay.Entities.Users GetUser(int id);
    }
}
