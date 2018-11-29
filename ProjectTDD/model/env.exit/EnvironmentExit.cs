using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTDD.model.env.exit
{
    public class EnvironmentExit : IEnvironmentExit
    {
        public void Exit(int code)
        {
            Environment.Exit(code);
        }
    }
}
