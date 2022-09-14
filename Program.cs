namespace MapCreator
{
    class Program
    {
        public static void Main()
        {
            Test game = new(1024, 512, "MapCreator");

            game.Run();
        }
    }
}