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

        static public void OnWindowKeyPress(object sender, KeyEventArgs e)
        {
            lastKeyPressed = e.Key;
            if (e.Key == Keys.Enter || e.Key == Keys.NumpadEnter)
            {
                DisplayManager.Window.Close();
            }
        }
        static public void MovePlayer() { }
        //--- Movment using NativeWindow.cs ---




        //--- Old Movment technique ---
        /*
        public static int W, A, S, D, P=1;
        public static void Callback(Window window, Keys key, int scanCode, InputState state, ModifierKeys mods)
        {
            switch (key)
            {
                case Keys.W:
                    if(state == InputState.Press)
                        W = 1;
                    else if(state == InputState.Release)
                        W = 0;
                    break;

                case Keys.A:
                    if (state == InputState.Press)
                        A = 1;
                    else if(state == InputState.Release)
                        A = 0;
                    break;

                case Keys.S:
                    if (state == InputState.Press)
                        S = 1;
                    else if (state == InputState.Release)
                        S = 0;
                    break;

                case Keys.D:
                    if (state == InputState.Press)
                        D = 1;
                    else if (state == InputState.Release)
                        D = 0;
                    break;

                case Keys.P:
                    if (state == InputState.Press)
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
