using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTDD
{
    public class App
    {
        public async void Exe()
        {
            view.IView v = new view.FarkleView();
            model.env.exit.IEnvironmentExit e = new model.env.exit.EnvironmentExit();
            controller.Farkle c = new controller.Farkle(v, e);
            await c.Start();
        }
    }
}
