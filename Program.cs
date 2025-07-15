using game;

namespace ProgramNamespace
{
    class Program
    {
        static void Main(string[] args)
        {
            FileInfo level_name = new FileInfo(args[0]);
            Level level = Parse_Level_File(level_name);
            level.DisplayInfo();
        }
        
        public static Level Parse_Level_File(FileInfo levelFile)
        {
            string[] lines = File.ReadAllLines(levelFile.FullName);

            string name = "";
            (int x, int y) mapSize = (0, 0);
            (int x, int y) startPos = (0, 0);
            (int x, int y) endPos = (0, 0);
            (int x, int y)[] path = [];
            char[,] grid = null!;
            List<Monster> enemies = new();
            List<Tower> towers = new();
            List<Wave> waves = new();
            int gold = 100;
            int lives = 20;

            int gridStartIndex = -1;

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (line.StartsWith("NAME:"))
                    name = line.Substring(5).Trim();
                else if (line.StartsWith("MAP_SIZE:"))
                {
                    var parts = line.Substring(9).Split('x');
                    mapSize = (int.Parse(parts[0]), int.Parse(parts[1]));
                }
                else if (line.StartsWith("START_POSITION:"))
                {
                    var parts = line.Substring(15).Split(',');
                    startPos = (int.Parse(parts[0]), int.Parse(parts[1]));
                }
                else if (line.StartsWith("END_POSITION:"))
                {
                    var parts = line.Substring(13).Split(',');
                    endPos = (int.Parse(parts[0]), int.Parse(parts[1]));
                }
                else if (line.StartsWith("PATH"))
                {
                    var pathStr = line.Split(':')[1].Trim();
                    var coords = pathStr.Split('-');
                    path = [.. coords.Select(c =>
                    {
                        var xy = c.Split(',');
                        return (int.Parse(xy[0]), int.Parse(xy[1]));
                    })];
                    // Grid empieza en la línea siguiente
                    gridStartIndex = i + 1;
                }
                else if (line.StartsWith("WAVE_"))
                {
                    var waveParts = line.Split(':');
                    int waveNum = int.Parse(waveParts[0].Replace("WAVE_", ""));
                    
                    // Unir todas las partes después del primer ':' para manejar múltiples ':'
                    string waveData = string.Join(":", waveParts.Skip(1));
                    var monstersInfo = waveData.Split(',');
                    
                    int delay = 1;
                    List<Monster> waveMonsters = [];
                    
                    foreach (var info in monstersInfo)
                    {
                        var trimmedInfo = info.Trim();
                        if (trimmedInfo.StartsWith("DELAY"))
                        {
                            delay = int.Parse(trimmedInfo.Split(':')[1]);
                        }
                        else if (trimmedInfo.Contains(':'))
                        {
                            var m = trimmedInfo.Split(':');
                            if (m.Length >= 2)
                            {
                                string type = m[0].Trim();
                                int count = int.Parse(m[1].Trim());
                                for (int k = 0; k < count; k++)
                                {
                                    waveMonsters.Add(type switch
                                    {
                                        "GOBLIN" => new Goblin(startPos),
                                        "ORC" => new Orc(startPos),
                                        "TROLL" => new Troll(startPos),
                                        "DEMON" => new Demon(startPos),
                                        "BOSS_DEMON" => new Boss_Demon(startPos),
                                        "ARCH_DEMON" => new Arch_Demon(startPos),
                                        _ => new Monster(type, 0, 0, startPos)
                                    });
                                }
                            }
                        }
                    }
                    waves.Add(new Wave(waveNum, waveMonsters.Count, delay) { Monsters = waveMonsters });
                }
                else if (line.StartsWith("STARTING_GOLD:"))
                    gold = int.Parse(line.Split(':')[1].Trim());
                else if (line.StartsWith("STARTING_LIVES:"))
                    lives = int.Parse(line.Split(':')[1].Trim());
            }

            // Procesar la grid si se encontró el inicio, gracias a Claudia
            if (gridStartIndex != -1 && mapSize.x > 0 && mapSize.y > 0)
            {
                grid = new char[mapSize.y, mapSize.x]; // [filas, columnas] -> [y, x] por como se guarda en memoria
                
                // Obtener solo las líneas válidas de la grid (sin comentarios ni líneas vacías)
                List<string> validGridLines = [];
                for (int i = gridStartIndex; i < lines.Length && validGridLines.Count < mapSize.y; i++)
                {
                    string gridLine = lines[i].Trim();
                    // Solo agregar líneas que no sean comentarios ni estén vacías y tengan la longitud correcta
                    if (!string.IsNullOrWhiteSpace(gridLine) && 
                        !gridLine.StartsWith("//") && 
                        gridLine.Length >= mapSize.x &&
                        gridLine.All(c => c == '0' || c == '1')) // Validar que solo contenga caracteres de grid
                    {
                        validGridLines.Add(gridLine);
                    }
                }
                
                if (validGridLines.Count < mapSize.y)
                {
                    throw new Exception($"No se encontraron suficientes líneas válidas para la grid. Se esperaban {mapSize.y} líneas, pero solo se encontraron {validGridLines.Count}.");
                }
                
                for (int y = 0; y < mapSize.y; y++)
                {
                    string gridLine = validGridLines[y];
                    for (int x = 0; x < mapSize.x; x++)
                    {
                        grid[y, x] = gridLine[x];
                    }
                }
            }

            var lvl = new Level(name, mapSize, grid, startPos, endPos, path, enemies, towers, waves, gold, lives);
            return lvl;
        }
    }
}