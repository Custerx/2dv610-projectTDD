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
            model.console.readline.IConsoleReadline cr = new model.console.readline.ConsoleReadline();
            view.IView v = new view.FarkleView(cr);
            model.env.exit.IEnvironmentExit e = new model.env.exit.EnvironmentExit();
            controller.IFarkle c = new controller.Farkle(v, e);
            await c.Start();
        }
    }
}
