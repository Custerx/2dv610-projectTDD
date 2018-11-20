using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTDD
{
    class Program
    {
        static void Main(string[] args)
        {
            view.FarkleView v = new view.FarkleView();
            controller.Farkle c = new controller.Farkle(v);
            c.Start();
        }
    }
}
