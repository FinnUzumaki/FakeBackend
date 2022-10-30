using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho
{
    public class Timer
    {
        private float oldTime, curTime, deltaTime, avgTime;
        private int i;

        public Timer()
        {
            oldTime = 0;
            curTime = 0;
            deltaTime = 0;
            avgTime = 0;
            i = 0;
        }

        public float Tick()
        {
            curTime = DateTime.Now.Millisecond;
            deltaTime = curTime - oldTime;
            avgTime += deltaTime;
            oldTime = curTime;
            i++;
            return deltaTime;
        }

        public float Avg()
        {
            avgTime /= (i != 0 ? i : 1);
            return avgTime;
        }
    }
}
