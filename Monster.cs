namespace game
{
    class Monster
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public (int x, int y) Position { get; set; }

        public Monster(string name, int health, int damage, (int x, int y) position)
        {
            Name = name;
            Health = health;
            Damage = damage;
            Position = position;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Monster: {Name}, Health: {Health}, Damage: {Damage}, Position: {Position.x},{Position.y}");
        }
    }

    // Subclasses for different types of monsters hardcoded with specific stats
    class Goblin : Monster
    {
        public Goblin((int x, int y) position) : base("Goblin", 20, 1, position)
        {
        }
    }
    class Orc : Monster
    {
        public Orc((int x, int y) position) : base("Orc", 50, 2, position)
        {
        }
    }
    class Troll : Monster
    {
        public Troll((int x, int y) position) : base("Troll", 100, 3, position)
        {
        }
    }
    class Demon : Monster
    {
        public Demon((int x, int y) position) : base("Demon", 250, 4, position)
        {
        }
    }
    class Boss_Demon : Monster
    {
        public Boss_Demon((int x, int y) position) : base("Boss Demon", 500, 7, position)
        {
        }
    }
    class Arch_Demon : Monster
    {
        public Arch_Demon((int x, int y) position) : base("Arch Demon", 1000, 10, position)
        {
        }
    }
}