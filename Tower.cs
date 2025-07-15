namespace game
{
    class Tower
    {
        public string Name { get; set; }
        public int Damage { get; set; }
        public int Range { get; set; }
        public int Cost { get; set; }
        public (int x, int y) Position { get; set; }

        public Tower(string name, int damage, int range, int cost, (int x, int y) position)
        {
            Name = name;
            Damage = damage;
            Range = range;
            Cost = cost;
            Position = position;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Tower: {Name}, Damage: {Damage}, Range: {Range}, Cost: {Cost}, Position: {Position.x},{Position.y}");
        }
    }
    // Subclasses for different types of towers hardcoded with specific stats
    class BasicTower : Tower
    {
        public BasicTower((int x, int y) position) : base("Basic Tower", 25, 3, 20, position)
        {
        }
    }
}