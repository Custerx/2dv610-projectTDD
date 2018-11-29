using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTDD.controller
{
    public interface IFarkle
    {
        Task Start(bool a_noTest = true);
    }
}
