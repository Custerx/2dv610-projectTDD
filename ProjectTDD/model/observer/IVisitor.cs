using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTDD.model.observer
{
    interface IVisitor
    {
        void VisitPlayer(IPlayer a_player);
    }
}
