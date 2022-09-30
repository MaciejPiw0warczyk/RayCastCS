using GLFW;
using System.Numerics;
using static OpenGL.GL;

namespace MapCreator.Rendering.Display
{
    static unsafe class DisplayManager
    {
        public static NativeWindow Window { get; set; }
        public static Vector2 WindowSize { get; set; }

        public static void CreateWindow(int width, int height, string title)
        {
            Glfw.Init();

            Glfw.WindowHint(Hint.ContextVersionMajor, 3);
            Glfw.WindowHint(Hint.ContextVersionMinor, 3);
            Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);
            Glfw.WindowHint(Hint.Resizable, false);
            Glfw.WindowHint(Hint.Focused, true);

            //Window = Glfw.CreateWindow(width, height, title, Monitor.None, Window.None);
            Window = new NativeWindow(width, height, title);

            //Glfw.MakeContextCurrent(Window);
            Window.MakeCurrent();
            Import(Glfw.GetProcAddress);

            WindowSize = new Vector2(width, height);

            glViewport(0, 0, width, height);
            Glfw.SwapInterval(1); //Vsync switch
        }

        public static void CloseWindow()
        {
            //Glfw.Terminate();
            Window.Dispose();
            Window.Close();
        }
    }
}

