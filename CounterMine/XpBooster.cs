namespace visualprogramming.CounterMine;

public class XpBooster
{
    private XpBoosterType _type;
    private double _multiplier;
    private DateTime _activationTime;
    private TimeSpan? _duration;
    
    public XpBoosterType Type => _type;
    public double Multiplier => _multiplier;
    public DateTime ActivationTime => _activationTime;
    public TimeSpan? Duration => _duration;

    public XpBooster(XpBoosterType type, double multiplier, TimeSpan? duration = null)
    {
        _type = type;
        _multiplier = multiplier;
        _duration = duration;
        _activationTime = DateTime.Now;
    }

    public bool IsActive()
    {
        if (Duration == null)
            return true; // Permanent XP Booster
        return DateTime.Now < ActivationTime + Duration.Value;
    }
    
    public override string ToString()
    {
        string durationText = Duration == null ? "Permanent" : $"{Duration.Value.TotalMinutes} min";
        return $"{Type} x{Multiplier} ({durationText})";
    }
}

public enum XpBoosterType
{
    Prime,
    Prestige,
    Temp
}