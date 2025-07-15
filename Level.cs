namespace game
{
    class Level
    {
        public string Name { get; set; }
        public (int x, int y) Map_Size { get; set; }
        public char[,] Grid { get; set; } // [y, x] for 2D grid representation
        public (int x, int y) Start_Position { get; set; }
        public (int x, int y) End_Position { get; set; }
        public (int x, int y)[] Path { get; set; }
        public int Gold { get; set; }
        public int Lives { get; set; }
        public List<Monster> Enemies { get; set; }
        public List<Tower> Towers { get; set; }
        public List<Wave> Waves { get; set; }

        public Level(string name, (int x, int y) mapSize, char[,] grid, (int x, int y) startPosition, (int x, int y) endPosition, (int x, int y)[] path, List<Monster>? enemies = null, List<Tower>? towers = null, List<Wave>? waves = null, int gold = 100, int lives = 20)
        {
            Name = name;
            Map_Size = mapSize;
            Grid = grid;
            Start_Position = startPosition;
            End_Position = endPosition;
            Path = path;
            Gold = gold;
            Lives = lives;
            Enemies = enemies ?? new List<Monster>();
            Towers = towers ?? new List<Tower>();
            Waves = waves ?? new List<Wave>();
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Level: {Name}, Size: {Map_Size.x}x{Map_Size.y}, Start: {Start_Position.x},{Start_Position.y}, End: {End_Position.x},{End_Position.y}");
            Console.WriteLine($"Gold: {Gold}, Lives: {Lives}");
            Console.WriteLine("Enemies:");
            foreach (var enemy in Enemies)
            {
                enemy.DisplayInfo();
            }
            Console.WriteLine("Towers:");
            foreach (var tower in Towers)
            {
                tower.DisplayInfo();
            }
            Console.WriteLine("Waves:");
            foreach (var wave in Waves)
            {
                wave.DisplayInfo();
            }
            Console.WriteLine("Grid:");
            for (int y = 0; y < Map_Size.y; y++)
            {
                for (int x = 0; x < Map_Size.x; x++)
                {
                    Console.Write(Grid[y, x]);
                }
                Console.WriteLine();
            }
        }
    }
}