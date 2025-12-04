namespace visualprogramming.CounterMine;

public class LevelProgressionModel
{
    // Средние значения активности за матч
    private const int MatchDurationMinutes = 20;
    private const int Kills = 10;
    private const int Assists = 3;
    private const int Plants = 2;
    private const int Mvps = 2;
    private const int RoundsPlayed = 16;

    private static readonly int[] HoursPerDayOptions = { 1, 2, 4 };
    private static readonly uint[] KeyLevels = { 1, 5, 10, 25, 40 };

    public static void RunSimulation()
    {
        var results = new List<SimulationResult>();
        SaveXpTableToCsv("level_xp_table.csv");

        foreach (var hoursPerDay in HoursPerDayOptions)
        foreach (var hasPrime in new[] { false, true })
        {
            var account = new Account(hasPrime);
            ulong totalXp = 0;
            double weeklyActivitySpent = 0;
            ulong totalHours = 0;
            uint xpThisHour = 0;

            var keyLevelsDict = new Dictionary<int, Dictionary<uint, double>>();
            for (var p = 0; p <= LevelProgression.MaxPrestigeLevel; p++)
                keyLevelsDict[p] = KeyLevels.ToDictionary(k => k, _ => -1.0);

            var prestigeDict = new Dictionary<int, double>();
            for (var p = 0; p <= LevelProgression.MaxPrestigeLevel; p++)
                prestigeDict[p] = -1;

            while (account.PrestigeLevel < LevelProgression.MaxPrestigeLevel)
            {
                totalHours += 1;

                xpThisHour = CalculateXpPerHour(account, weeklyActivitySpent);
                totalXp += xpThisHour;

                var activityXpThisHour = Math.Min(LevelProgression.WeeklyBonusXp - weeklyActivitySpent,
                    Kills * (int)XpPerActivity.Kill +
                    Assists * (int)XpPerActivity.Assist +
                    Plants * (int)XpPerActivity.Plant +
                    Mvps * (int)XpPerActivity.Mvp +
                    RoundsPlayed * (int)XpPerActivity.PlayedRound
                );
                activityXpThisHour = Math.Max(activityXpThisHour, 0);
                weeklyActivitySpent += activityXpThisHour;

                var prevLevel = account.CurrentLevel;
                int prevPrestige = account.PrestigeLevel;

                account.AddXp(xpThisHour);

                if (account.PrestigeLevel > prevPrestige)
                {
                    prestigeDict[account.PrestigeLevel] = totalHours;
                    keyLevelsDict[account.PrestigeLevel - 1][40] = totalHours;
                }

                foreach (var level in KeyLevels)
                    if (account.CurrentLevel >= level && keyLevelsDict[account.PrestigeLevel][level] < 0)
                        keyLevelsDict[account.PrestigeLevel][level] = totalHours;

                if (totalHours % (ulong)(7 * hoursPerDay) == 0)
                    weeklyActivitySpent = 0;
            }

            var keyLevelsStr = string.Join("; ",
                keyLevelsDict.SelectMany(p =>
                    p.Value.Select(l =>
                    {
                        if (l.Value < 0)
                            return $"P{p.Key}-L{l.Key}=-";
                        else
                            return $"P{p.Key}-L{l.Key}={(l.Value / hoursPerDay):F0}";
                    })));

            var prestigesStr = string.Join("; ",
                prestigeDict.Select(p =>
                {
                    if (p.Value < 0)
                        return $"P{p.Key}=-";
                    else
                        return $"P{p.Key}={(p.Value / hoursPerDay):F0}";
                }));


            results.Add(new SimulationResult
            {
                Scenario = $"{hoursPerDay} h/day {(hasPrime ? "(Prime)" : "(No Prime)")}",
                HoursPerDay = hoursPerDay,
                HasPrime = hasPrime,
                HoursToPrestige = totalHours,
                DaysToPrestige = totalHours / (ulong)hoursPerDay,
                TotalXp = totalXp,
                KeyLevelsReached = keyLevelsStr,
                PrestigesReached = prestigesStr
            });

            SaveSimulationToCsv(results, "simulation_results.csv");
        }
    }


    private static uint CalculateXpPerHour(Account account, double weeklyActivityXpSpent)
    {
        var activityXp =
            Kills * (uint)XpPerActivity.Kill +
            Assists * (uint)XpPerActivity.Assist +
            Plants * (uint)XpPerActivity.Plant +
            Mvps * (uint)XpPerActivity.Mvp +
            RoundsPlayed * (uint)XpPerActivity.PlayedRound;

        var activityXpThisHour = (uint)Math.Min(LevelProgression.WeeklyBonusXp - weeklyActivityXpSpent, activityXp);
        activityXpThisHour = Math.Max(activityXpThisHour, 0);

        var timeXp = MatchDurationMinutes * (uint)XpPerMinute.Casual;

        ulong totalXpPerMatch = timeXp + activityXpThisHour;

        var matchesPerHour = 60.0 / MatchDurationMinutes;

        var xpPerHour = (uint)(totalXpPerMatch * matchesPerHour * account.GetActiveXpMultiplier());
        return xpPerHour;
    }

    private static void SaveSimulationToCsv(List<SimulationResult> results, string path)
    {
        using var writer = new StreamWriter(path);
        writer.WriteLine("Scenario,HoursPerDay,HasPrime,HoursToPrestige,DaysToPrestige,TotalXp,KeyLevels,Prestiges");

        foreach (var res in results)
            writer.WriteLine($"{res.Scenario},{res.HoursPerDay},{res.HasPrime}," +
                             $"{res.DaysToPrestige}," +
                             $"{res.KeyLevelsReached},{res.PrestigesReached}");
    }

    private static void SaveXpTableToCsv(string filePath)
    {
        using var writer = new StreamWriter(filePath);
        writer.WriteLine("Level,XPToUpgrade,TotalXPToLevel");
        for (uint level = 1; level <= LevelProgression.LevelsToPrestige; level++)
        {
            var xpToUpgrade = LevelProgression.GetXpToUpgradeForLevel(level);
            var totalXp = LevelProgression.GetTotalXpToLevel(level);
            writer.WriteLine($"{level},{xpToUpgrade},{totalXp}");
        }
    }

    public class SimulationResult
    {
        public string Scenario { get; set; } = "";
        public double HoursPerDay { get; set; }
        public bool HasPrime { get; set; }
        public double HoursToPrestige { get; set; }
        public double DaysToPrestige { get; set; }
        public double TotalXp { get; set; }
        public string KeyLevelsReached { get; set; } = "";
        public string PrestigesReached { get; set; } = "";
    }
}