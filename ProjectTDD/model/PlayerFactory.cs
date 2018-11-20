using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTDD.model
{
    class PlayerFactory
    {
        public IPlayer CreateNewPlayer()
        {
            return new Player(CreateNewHand());
        }

        public IHand CreateNewHand()
        {
            return new Hand();
        }
    }
}
