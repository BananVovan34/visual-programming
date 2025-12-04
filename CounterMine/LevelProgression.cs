namespace visualprogramming.CounterMine;

public class LevelProgression
{
    public const uint BaseXp = 1000;
    public const uint IncrementXp = 50;
    public const uint WeeklyBonusXp = 1000;

    public const uint LevelsToPrestige = 40;
    public const uint MaxPrestigeLevel = 5;
    
    /// <summary>
    /// Return xp value to upgrade selected level from previous
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public static uint GetXpToUpgradeForLevel(uint level)
    {
        if (level == 0) return 0;
        return BaseXp + (level - 1) * IncrementXp;
    }

    /// <summary>
    /// Return total xp to upgrade selected level
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public static uint GetTotalXpToLevel(uint level)
    {
        if (level == 0) return 0;
        return level * (2 * BaseXp + (level - 1) * IncrementXp) / 2; // Summary of arithmetic progression
    }

    public static bool IsAbleToGetNewPrestige(uint currentLevel)
    {
        return currentLevel >= LevelsToPrestige;
    }

    public static double GetPrestigeMultiplier(ushort prestigeLevel)
    {
        return prestigeLevel switch
        {
            0 => 1.0,
            1 => 1.1,
            2 => 1.2,
            3 => 1.25,
            4 => 1.375,
            5 => 1.5,
            _ => 1.0
        };
    }
}

public enum XpPerActivity
{
    Kill = 2,
    Assist = 1,
    Plant = 1,
    Mvp = 1,
    RankedWin = 25,
    RankedPlayedRound = 3,
    PlayedRound = 1
}

public enum XpPerMinute
{
    Ranked = 15,
    Casual = 12,
    Wingman = 10,
    Deathmatch = 8,
    ArmsRace = 8
}