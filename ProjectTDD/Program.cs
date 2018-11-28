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
            controller.Farkle c = new controller.Farkle(v);
            await c.Start();
        }
    }
}
