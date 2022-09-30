namespace MapCreator
{
    class Program
    {
        public static void Main()
        {
            Test game = new(1280, 562, "MapCreator");

            game.Run();

            MapCreator.Rendering.Display.DisplayManager.CloseWindow();
        }
    }
}