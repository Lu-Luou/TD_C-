namespace game
{
    class Wave
    {
        public int WaveNumber { get; set; }
        public int MonsterCount { get; set; }
        public int Delay { get; set; } // Delay between monsters
        public List<Monster> Monsters { get; set; }

        public Wave(int waveNumber, int monsterCount, int delay = 1)
        {
            WaveNumber = waveNumber;
            MonsterCount = monsterCount;
            Delay = delay;
            Monsters = new List<Monster>();
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Wave {WaveNumber}: {MonsterCount} monsters, Delay: {Delay}");
        }

        public void SuffleMonsters()
        {
            Random rand = new Random();
            Monsters = [.. Monsters.OrderBy(x => rand.Next())];
        }
    }
}