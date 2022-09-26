using System;
using System.Collections.Generic;
using System.Numerics;
using MapCreator.Rendering.Display;
using GLFW;
using static OpenGL.GL;

namespace MapCreator.Wolf3D
{
    static class Rays
    {
        static List<float> RayList= new();
        static public List<float> SWF = new();

        public static List<float> DrawWalls()
        {
            RayList.Clear();
            SWF.Clear();

            Vector3 col = Vector3.Zero;
            int mx, my, mp, dof;
            double rx = 0, ry = 0, ra, xo = 0, yo = 0, aTan, FinalDis = 0;

            ra = Player.angle - Radians(1f) * 30;
            if (ra < 0) { ra += 2 * Math.PI; }
            if (ra > 2 * Math.PI) { ra -= 2 * Math.PI; }
            for (int i = 0; i < 240; i++)
            {
                //--Horizontal check--
                dof = 0;
                aTan = -1 / Math.Tan(ra);
                if (ra == 0 || ra == Math.PI) { rx = Player.X; ry = Player.Y; dof = 8; }
                if (ra > Math.PI) { ry = (((int)Player.Y / 64) * 64) - 0.0001; rx = (Player.Y - ry) * aTan + Player.X; yo = -64; xo = -yo * aTan; }
                if (ra < Math.PI) { ry = (((int)Player.Y / 64) * 64) + 64; rx = (Player.Y - ry) * aTan + Player.X; yo = 64; xo = -yo * aTan; }
                while (dof < 8)
                {
                    mx = (int)(rx) / 64; my = (int)(ry) / 64; mp = my * Maps.mapX + mx;
                    if (mp > 0 && mp < Maps.mapX * Maps.mapY && Maps.wallMap[mp] == 1) { break; }
                    else { rx += xo; ry += yo; dof++; }
                }
                Vector2 HorPoint = new ((int)rx, (int)ry);
                //--------------------

                //--Vertical check--
                dof = 0;
                aTan = -Math.Tan(ra);
                if (ra == Math.PI / 2 || ra == 3 * Math.PI / 2) { rx = Player.X; ry = Player.Y; dof = 8; }
                if (ra > Math.PI / 2 && ra < 3 * Math.PI / 2) { rx = (((int)Player.X / 64) * 64) - 0.0001; ry = (Player.X - rx) * aTan + Player.Y; xo = -64; yo = -xo * aTan; }
                if (ra < Math.PI / 2 || ra > 3 * Math.PI / 2) { rx = (((int)Player.X / 64) * 64) + 64; ry = (Player.X - rx) * aTan + Player.Y; xo = 64; yo = -xo * aTan; }
                while (dof < 8)
                {
                    mx = (int)(rx) / 64; my = (int)(ry) / 64; mp = my * Maps.mapX + mx;
                    if (mp > 0 && mp < Maps.mapX * Maps.mapY && Maps.wallMap[mp] == 1) { break; }
                    else { rx += xo; ry += yo; dof++; }
                }
                Vector2 VerPoint = new ((int)rx, (int)ry);
                //------------------

                double DistHor = Pitagoras(Player.pos, HorPoint);
                double DistVer = Pitagoras(Player.pos, VerPoint);

                if (DistHor < DistVer ) 
                {
                    float[] vert =
                          {
                        Player.X,Player.Y, 0f, 0f, 1f,
                        HorPoint.X,HorPoint.Y, 0f, 0f, 1f,
                    }; 
                    RayList.AddRange(vert);
                    FinalDis = DistHor; col = new Vector3(48/255f, 40/255f, 166/255f);
                }
                else if (DistVer < DistHor )
                {
                    float[] vert =
                          {
                        Player.X,Player.Y, 1f, 0f, 0f,
                        VerPoint.X,VerPoint.Y, 1f, 0f, 0f,
                    };
                    RayList.AddRange(vert);
                    FinalDis = DistVer; col = new Vector3(89/255f, 79/255f, 240/255f);
                }



                double fishFix = Player.angle - ra; if (fishFix < 0) { fishFix += 2 * Math.PI; }
                if (fishFix > 2 * Math.PI) { fishFix -= 2 * Math.PI; }
                FinalDis *= Math.Cos(fishFix);
                float lineH = Maps.mapS * 320 / (float)FinalDis;
                float lineOffset = 256 - lineH / 2;




                float[] Floor =
                {
                    i*2+530,     DisplayManager.WindowSize.Y, .6f,.4f,.5f,
                    i*2+530,     lineOffset+lineH,            .6f,.4f,.5f,
                    (i+1)*2+530, lineOffset+lineH,            .6f,.4f,.5f,

                    (i+1)*2+530, lineOffset+lineH,            .6f,.4f,.5f,
                    (i+1)*2+530, DisplayManager.WindowSize.Y, .6f,.4f,.5f,
                    i*2+530,     DisplayManager.WindowSize.Y, .6f,.4f,.5f,
                };
                SWF.AddRange(Floor);
                //FillPolygon(Floor, Vector3.Presets.Mint);
                //DrawLine(new Vector2(i *2+530, DisplayManager.WindowSize.Y), new Vector2(i * 4 + 530, (int)(lineOffset + lineH)), Vector3.Presets.Red);

                float[] Sealing =
                {
                    i*2+530,      0,          0,1,0,
                    i*2+530,      lineOffset, 0,1,0,
                    (i+1)*2+ 530, lineOffset, 0,1,0,

                    (i+1)*2+ 530, lineOffset, 0,1,0,
                    (i+1)*2+530,  0,          0,1,0,
                    i*2+530,      0,          0,1,0,

                };
                SWF.AddRange(Sealing);
                //FillPolygon(Sealing, Vector3.Presets.DarkGreen);
                //DrawLine(new Vector2(i * 4 + 530, 0), new Vector2(i * 4 + 530, (int)(lineOffset + 0)), Vector3.Presets.Yellow);


                float[] Wall =
                {
                    i*2+530,     lineOffset,         col.X, col.Y,col.Z,
                    i*2+530,     lineH + lineOffset, col.X, col.Y,col.Z,
                    (i+1)*2+530, lineH + lineOffset, col.X, col.Y,col.Z,

                    (i+1)*2+530, lineH + lineOffset, col.X, col.Y,col.Z,
                    i*2+530,     lineOffset,         col.X, col.Y,col.Z,
                    (i+1)*2+530, lineOffset,         col.X, col.Y,col.Z,
                };
                SWF.AddRange(Wall);
                //FillPolygon(Wall, col);



                ra += Radians(.25f);
                if (ra < 0) { ra += 2 * Math.PI; }
                if (ra > 2 * Math.PI) { ra -= 2 * Math.PI; }

            }
            return RayList;
        }
            static double Pitagoras(Vector2 A, Vector2 B)
            {
                var X = Math.Abs(A.X - B.X);
                var Y = Math.Abs(A.Y - B.Y);
                var D = X * X + Y * Y;
                var r = Math.Sqrt((double)D);
                return r;
            }
            static double Radians(double angle)
            {
                return (Math.PI / 180) * angle;
            }
    }
}