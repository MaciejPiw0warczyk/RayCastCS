using MapCreator.Rendering.Display;
using GLFW;

namespace MapCreator.Game
{
    abstract class Game
    {

        protected int InitialWindowWidth { get; set; }
        protected int InitialWindowHeight { get; set;}
        protected string InitialWindowTitle { get; set; }
        protected Game(int initialWindowWidth, int initialWindowHeight, string initialWindowTitle)
        {
            InitialWindowWidth = initialWindowWidth;
            InitialWindowHeight = initialWindowHeight;
            InitialWindowTitle = initialWindowTitle;
        }


        public void Run() 
        { 
            Initialize();

            DisplayManager.CreateWindow(InitialWindowWidth,InitialWindowHeight,InitialWindowTitle);
            //Glfw.SetKeyCallback(DisplayManager.Window, KeyCallback);
            SubscribeToWindowEvents();

            LoadContent();

            
            while (!Glfw.WindowShouldClose(DisplayManager.Window))
            {
                GameTime.DeltaTime = (float)Glfw.Time - GameTime.TotalElapsedSec;   
                GameTime.TotalElapsedSec = (float)Glfw.Time;

                Glfw.PollEvents();

                Update();


                Render();
            }

            DisplayManager.CloseWindow();
        }





        //protected abstract void KeyCallback(Window window, Keys key, int scanCode, InputState state, ModifierKeys mods);
        private static void SubscribeToWindowEvents()
        {
            DisplayManager.Window.KeyAction += Wolf3D.Movment.OnWindowKeyAction;
            //DisplayManager.Window.SizeChanged += Program.OnWindowsSizeChanged;
            //DisplayManager.Window.Refreshed += Program.OnWindowRefreshed;
            //DisplayManager.Window.MouseMoved += Program.OnWindowMouseMoved;
        }
        protected abstract void Initialize();
        protected abstract void LoadContent();

        protected abstract void Update();
        protected abstract void Render();
    }
}
