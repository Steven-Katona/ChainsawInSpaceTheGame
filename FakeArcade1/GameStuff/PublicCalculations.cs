using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeArcade1.GameStuff
{
    public class PublicCalculations
    {
        public (float, float) newDirection(int x, int y, Vector2 terminal) //used for determining projectile direction. 
        {
            double dirX = terminal.X - (double)x;
            double dirY = terminal.Y - (double)y;
            double length = Math.Sqrt(Math.Pow(dirY, 2) + Math.Pow(dirX, 2));
            dirX = dirX / length;
            dirY = dirY / length;
            

            (float, float) direction = ((float)dirX, (float)dirY);
            return direction; 
        }
    }
}
