using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreator.Wolf3D
{
    internal class Colision
    {
        static public Vector2 ColisionPoint, NegativeColisionPoint;


        static public bool Coliding(Vector2 ColisionPoint)
        {
            int mx = (int)ColisionPoint.X / 64; int my = (int)ColisionPoint.Y / 64; int mp = my * Maps.mapX + mx;
            if (mp > 0 && mp < Maps.mapX * Maps.mapY && Maps.ammoMap[mp] == 1) { Maps.ammoMap[mp] = 0; Player.ammo += 10; }
            if (mp > 0 && mp < Maps.mapX * Maps.mapY && Maps.wallMap[mp] == 1) { return true; }
            else { return false; }
        }
    }
}
