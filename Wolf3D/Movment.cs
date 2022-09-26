using System;
using GLFW;
using MapCreator.Rendering.Display;
using static OpenGL.GL;

namespace MapCreator.Wolf3D
{
    static class Movment
    {
        //--- Movment using NativeWindow.cs ---
        private static Keys? lastKeyPressed;
        public static int W, A, S, D, P = 1;

        static public void OnWindowKeyAction(object sender, KeyEventArgs e)
        {

            switch (e.Key)
            {
                case Keys.W:
                    if (e.State== InputState.Press)
                        W = 1;
                    else if (e.State == InputState.Release)
                        W = 0;
                    break;

                case Keys.A:
                    if (e.State == InputState.Press)
                        A = 1;
                    else if (e.State == InputState.Release)
                        A = 0;
                    break;

                case Keys.S:
                    if (e.State == InputState.Press)
                        S = 1;
                    else if (e.State == InputState.Release)
                        S = 0;
                    break;

                case Keys.D:
                    if (e.State == InputState.Press)
                        D = 1;
                    else if (e.State == InputState.Release)
                        D = 0;
                    break;

                case Keys.P:
                    if (e.State == InputState.Press)
                        P++;
                    if (P % 2 == 0)
                        Test.musicChannel.Play();
                    else
                        Test.musicChannel.Pause();
                    Console.WriteLine(P);
                    Console.WriteLine(P % 2);
                    break;

                default: break;
            }

        }
        static public void MovePlayer()
        {
            Player.deltaX = (float)Math.Cos(Player.angle) * 1.5f;
            Player.deltaY = (float)(Math.Sin(Player.angle) * 1.5f);

            if (W == 1)
                if (!Colision.Coliding(Colision.ColisionPoint))
                { Player.X += Player.deltaX; Player.Y += Player.deltaY; }

            if (A == 1)
            { Player.angle -= 0.05f; if (Player.angle < 0) { Player.angle += 2 * (float)Math.PI; } if (Player.angle > Math.PI * 2) { Player.angle -= 2 * (float)Math.PI; } }

            if (S == 1)
                if (!Colision.Coliding(Colision.NegativeColisionPoint))
                { Player.X -= Player.deltaX; Player.Y -= Player.deltaY; }

            if (D == 1)
            { Player.angle += 0.05f; if (Player.angle > Math.PI * 2) { Player.angle -= 2 * (float)Math.PI; } if (Player.angle < 0) { Player.angle += 2 * (float)Math.PI; } }
        }
        //--- Movment using NativeWindow.cs ---




        //--- Old Movment technique ---
        /*
        public static int W, A, S, D, P=1;
        public static void Callback(Window window, Keys key, int scanCode, InputState e.State, ModifierKeys mods)
        {
            switch (key)
            {
                case Keys.W:
                    if(e.State == InputState.Press)
                        W = 1;
                    else if(e.State == InputState.Release)
                        W = 0;
                    break;

                case Keys.A:
                    if (e.State == InputState.Press)
                        A = 1;
                    else if(e.State == InputState.Release)
                        A = 0;
                    break;

                case Keys.S:
                    if (e.State == InputState.Press)
                        S = 1;
                    else if (e.State == InputState.Release)
                        S = 0;
                    break;

                case Keys.D:
                    if (e.State == InputState.Press)
                        D = 1;
                    else if (e.State == InputState.Release)
                        D = 0;
                    break;

                case Keys.P:
                    if (e.State == InputState.Press)
                        P ++;
                    if(P%2==0)
                        Test.musicChannel.Play();
                    else
                        Test.musicChannel.Pause();
                    Console.WriteLine(P);
                    Console.WriteLine(P%2);
                    break;

                default: break;
    }


    static public void MovePlayer()
        {
            Player.deltaX = (float)Math.Cos(Player.angle) * 1.5f; 
            Player.deltaY = (float)(Math.Sin(Player.angle) * 1.5f);

            if (W==1)
                if (!Colision.Coliding(Colision.ColisionPoint))
                    { Player.X += Player.deltaX; Player.Y += Player.deltaY; }

            if (A == 1)
                { Player.angle -= 0.05f; if (Player.angle < 0) { Player.angle += 2 * (float)Math.PI; } if (Player.angle > Math.PI * 2) { Player.angle -= 2 * (float)Math.PI; } }

            if(S==1)
                if (!Colision.Coliding(Colision.NegativeColisionPoint))
                    { Player.X -= Player.deltaX; Player.Y -= Player.deltaY; }

            if (D == 1)
                { Player.angle += 0.05f; if (Player.angle > Math.PI * 2) { Player.angle -= 2 * (float)Math.PI; } if (Player.angle < 0) { Player.angle += 2 * (float)Math.PI; } }
        }
            }*/
        //--- Old Movment technique ---
    }
}
