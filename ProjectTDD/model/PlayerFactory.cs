using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTDD.model
{
    public class PlayerFactory
    {
        public virtual IPlayer CreateNewPlayer()
        {
            return new Player(CreateNewHand());
        }

        public IHand CreateNewHand()
        {
            return new Hand();
        }
    }
}
