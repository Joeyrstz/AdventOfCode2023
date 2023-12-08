using System.Reflection;

namespace SolvingLogic.Day_5;

public static class Day5Solver
{
    public static string[] GetInput()
    {
        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        path = Path.Combine(path, "Day 5", "Input.txt");
        return File.ReadAllLines(path);
    }

    public static long SolveTask1(string[] lines)
    {
        var seedLine = lines[0];
        seedLine = seedLine.Replace("seeds: ", string.Empty);
        var seeds = seedLine.Split(" ").Select(long.Parse).ToList();

        var mappingLines = lines[2..];

        return MinimumLocation(seeds.ToArray(), mappingLines);
    }

    public static long SolveTask2Legacy(string[] lines)
    {
        var seedLine = lines[0];
        seedLine = seedLine.Replace("seeds: ", string.Empty);

        var seeds = new List<long>();
        var seedBoxes = seedLine.Split(" ");
        for (int i = 0; i < seedBoxes.Length; i+= 2)
        {
            var start = long.Parse(seedBoxes[i]);
            var range = long.Parse(seedBoxes[i + 1]);
            for (int j = 1; j <= range; j++)
            {
                seeds.Add(start);
                start++;
            }
        }
        
        
        var mappingLines = lines[2..];
        return MinimumLocation(seeds.ToArray(), mappingLines);
    }
    
    public static long SolveTask2(string[] lines)
    {
        var seedLine = lines[0];
        seedLine = seedLine.Replace("seeds: ", string.Empty);

        var seedRanges = new List<Tuple<long, long>>();
        var seedBoxes = seedLine.Split(" ");
        for (int i = 0; i < seedBoxes.Length; i+= 2)
        {
            var min = long.Parse(seedBoxes[i]);
            var max = min + long.Parse(seedBoxes[i + 1]) - 1;
            seedRanges.Add(new Tuple<long, long>(min, max));
        }
        
        
        var mappingLines = lines[2..];
        return MinimumLocationMultithread(seedRanges.ToArray(), mappingLines);
    }
    
    public static long MinimumLocation(long[] seeds, string[] mappingLines)
    {
        var mappingObjects = new List<AlamanacWrapper>();
        var header = true;
        AlamanacWrapper currentWrapper = null;
        foreach (var mappingLine in mappingLines)
        {
            if(header)
            {
                currentWrapper = new AlamanacWrapper(mappingLine.Replace("map:", string.Empty));
                header = false;
                continue;
            }

            if (string.IsNullOrWhiteSpace(mappingLine))
            {
                mappingObjects.Add(currentWrapper);
                header = true;
                continue;
            }

            currentWrapper.Maps.Add(new AlmanacMap(mappingLine));
        }
        mappingObjects.Add(currentWrapper);

        var minimum = long.MaxValue;
        foreach (var currentSeed in seeds)
        {
            var location = currentSeed;
            foreach (var currentMapper in mappingObjects)
            {
                foreach (var currentMap in currentMapper.Maps)
                {
                    if (currentMap.DoesValueFit(location) is long result)
                    {
                        location = result;
                        break;
                    }
                }
            }

            if (location < minimum)
            {
                minimum = location;
                Console.WriteLine("New minimum: " + minimum);
            }
        }
        return minimum;
    }
    
    public static long MinimumLocation(Tuple<long, long>[] seedRanges, string[] mappingLines)
    {
        var mappingObjects = new List<AlamanacWrapper>();
        var header = true;
        AlamanacWrapper currentWrapper = null;
        foreach (var mappingLine in mappingLines)
        {
            if(header)
            {
                currentWrapper = new AlamanacWrapper(mappingLine.Replace("map:", string.Empty));
                header = false;
                continue;
            }

            if (string.IsNullOrWhiteSpace(mappingLine))
            {
                mappingObjects.Add(currentWrapper);
                header = true;
                continue;
            }

            currentWrapper.Maps.Add(new AlmanacMap(mappingLine));
        }
        mappingObjects.Add(currentWrapper);

        var minimum = long.MaxValue;
        foreach (var currentSeed in seedRanges)
        {
            var (min, max) = currentSeed;
            for(long i = min; i <= max; i++)
            {
                var location = min;
                foreach (var currentMapper in mappingObjects)
                {
                    foreach (var currentMap in currentMapper.Maps)
                    {
                        if (currentMap.DoesValueFit(location) is not long result) continue;
                        location = result;
                        break;
                    }
                }

                if (location >= minimum) continue;
                
                minimum = location;
                Console.WriteLine("New minimum: " + minimum);
            }
        }
        return minimum;
    }

    public static long MinimumLocationMultithread(Tuple<long, long>[] seedRanges, string[] mappingLines)
    {
        var mappingObjects = new List<AlamanacWrapper>();
        var header = true;
        AlamanacWrapper currentWrapper = null;
        foreach (var mappingLine in mappingLines)
        {
            if(header)
            {
                currentWrapper = new AlamanacWrapper(mappingLine.Substring(4));
                header = false;
                continue;
            }

            if (string.IsNullOrWhiteSpace(mappingLine))
            {
                mappingObjects.Add(currentWrapper);
                header = true;
                continue;
            }

            currentWrapper.Maps.Add(new AlmanacMap(mappingLine));
        }
        mappingObjects.Add(currentWrapper);

        long minimum = long.MaxValue;

        // Parallel processing
        Parallel.ForEach(seedRanges, currentSeed =>
        {
            var (min, max) = currentSeed;
            for(long i = min; i <= max; i++)
            {
                var location = i;
                foreach (var currentMapper in mappingObjects)
                {
                    foreach (var currentMap in currentMapper.Maps)
                    {
                        if (currentMap.DoesValueFit(location) is long result)
                        {
                            location = result;
                            break;
                        }
                    }
                }

                // Compare with the current minimum
                lock (mappingObjects)
                {
                    if (location < minimum)
                    {
                        minimum = location;
                        Console.WriteLine("New minimum: " + minimum);
                    }
                }
            }
        });

        return minimum;
    }
    
}