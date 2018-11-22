using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTDD.model.observer
{
    interface IVisitable
    {
        void Accept(IVisitor a_visitor);
    }
}
