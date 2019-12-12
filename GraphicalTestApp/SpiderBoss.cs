using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GraphicalTestApp
{
    class SpiderBoss : Enemy
    {
        Stopwatch stopwatch = new Stopwatch();

        public SpiderBoss(float x, float y) : base(x, y)
        {
            stopwatch.Start();
            OnUpdate += Web;
        }

        public void Web(float deltaTime)
        {
            if (stopwatch.ElapsedMilliseconds > 1000)
            {
                Web web = new Web(16, 16);

                web.YAcceleration = 5f;

                web.X = X - 1;
                web.Y = Y - 1;

                Parent.AddChild(web);

                stopwatch.Restart();
            }
        }
    }
}
