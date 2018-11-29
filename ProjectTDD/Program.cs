using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTDD
{
    class Program
    {
        static async Task Main(string[] args)
        {
            view.IView v = new view.FarkleView();
            model.env.exit.IEnvironmentExit e = new model.env.exit.EnvironmentExit();
            controller.IFarkle c = new controller.Farkle(v, e);
            await c.Start();
        }
    }
}
