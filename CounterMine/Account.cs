namespace visualprogramming.CounterMine;

public class Account
{
    private static ulong _accountIdSet = 0;
    
    private ulong _accoundId;
    private uint _currentLevel;
    private uint _currentXp;
    private ulong _totalXp;
    private ushort _prestigeLevel;
    private readonly List<XpBooster> _activeBoosters = new();
    
    public ulong AccountId => _accoundId;
    public uint CurrentLevel => _currentLevel;
    public uint CurrentXp => _currentXp;
    public ulong TotalXp => _totalXp;
    public uint XpToUpgrade => LevelProgression.GetXpToUpgradeForLevel(_currentLevel + 1);
    public ushort PrestigeLevel => _prestigeLevel;
    public IReadOnlyList<XpBooster> ActiveBoosters => _activeBoosters.AsReadOnly();

    public Account(bool hasPrime = false)
    {
        _accoundId = _accountIdSet++;
        _currentLevel = 0;
        _currentXp = 0;
        _totalXp = 0;
        _prestigeLevel = 0;
        
        if (hasPrime)
            AddBooster(new XpBooster(XpBoosterType.Prime, 1.5));
        
        AddPrestigeBooster();
    }

    /// <summary>
    /// Add new booster into Active Boosters List
    /// </summary>
    /// <param name="booster"></param>
    public void AddBooster(XpBooster booster)
    {
        _activeBoosters.Add(booster);
    }

    /// <summary>
    /// Processing total XP multiplier
    /// </summary>
    /// <returns></returns>
    public double GetActiveXpMultiplier()
    {
        double multiplier = 1.0;

        foreach (var booster in _activeBoosters)
        {
            if (booster.IsActive())
                multiplier *= booster.Multiplier;
        }
        
        return multiplier;
    }

    /// <summary>
    /// Add XP to account
    /// </summary>
    /// <param name="baseXp"></param>
    public void AddXp(uint baseXp)
    {
        double totalMultiplier = GetActiveXpMultiplier();
        uint finalXp = (uint)Math.Ceiling(baseXp * totalMultiplier);
        
        _currentXp += finalXp;
        _totalXp += finalXp;
        
        CheckIsLevelUp();
    }

    /// <summary>
    /// Check is Level Up
    /// </summary>
    private void CheckIsLevelUp()
    {
        while (_currentXp >= LevelProgression.GetXpToUpgradeForLevel(_currentLevel + 1))
        {
            uint needed = LevelProgression.GetXpToUpgradeForLevel(_currentLevel + 1);
            _currentXp -= needed;
            _currentLevel++;
            Console.WriteLine($"Level {_currentLevel} has been upgraded.");
            
            GetNewPrestigeLevel();
        }
    }

    public void UpdateActiveXpBoosters()
    {
        foreach (var booster in new List<XpBooster>(_activeBoosters))
        {
            if (!booster.IsActive())
                _activeBoosters.Remove(booster);
        }
    }

    public void RemovePrestigeBoosters()
    {
        _activeBoosters.RemoveAll(booster => booster.Type == XpBoosterType.Prestige);
    }

    private bool GetNewPrestigeLevel()
    {
        if (LevelProgression.IsAbleToGetNewPrestige(CurrentLevel) && PrestigeLevel < LevelProgression.MaxPrestigeLevel)
        {
            _prestigeLevel++;
            ResetLevelProgression();
            
            Console.WriteLine($"Prestige {_prestigeLevel} has been upgraded. Level progression was reset.");

            AddPrestigeBooster();
            
            return true;
        }
        
        return false;
    }

    private void AddPrestigeBooster()
    {
        RemovePrestigeBoosters();
        
        double prestigeMultiplier = LevelProgression.GetPrestigeMultiplier(_prestigeLevel);
        AddBooster(new XpBooster(XpBoosterType.Prestige, prestigeMultiplier));
    }

    private void ResetLevelProgression()
    {
        _currentXp = 0;
        _currentLevel = 0;
    }

    private void ResetPrestigeLevel()
    {
        _prestigeLevel = 0;
    }

    private void FullReset()
    {
        ResetPrestigeLevel();
        ResetLevelProgression();
    }

    public override string ToString()
    {
        string boosters = _activeBoosters.Count == 0
            ? "No active boosters"
            : string.Join(", ", _activeBoosters.Select(booster => booster.ToString()));
        
        return $"Аккаунт {_accoundId}\n" +
               $"Уровень: {_currentLevel}\n" +
               $"Текущий XP: {_currentXp}\n" +
               $"Всего XP: {_totalXp}\n" +
               $"Бустеры: {boosters}";
    }
}