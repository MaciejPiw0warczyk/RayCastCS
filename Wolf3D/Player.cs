using System.Numerics;

namespace MapCreator.Wolf3D
{
    static class Player
    {
        public static float X, Y, angle, deltaX =.1f, deltaY=.1f;
        public static int ammo;

        static public Vector2 pos = new((int)X, (int)Y);
    }
}
