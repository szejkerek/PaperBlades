public class UpgradableStat : IUpgradable, IDisplayable
{
    public string IconGUID { get;}
    public string DisplayName { get;}
    public int MaxLevel { get; }
    public int Level { get; set; }
    public int Cost { get; }

    #region Constructors
    public UpgradableStat(UpgradableStatSO data) : this(data.Icon.AssetGUID, data.DisplayName, data.MaxLevel, data.Cost, currentLevel: 0) {  }

    public UpgradableStat(string IconGUID, string displayName, int maxLevel, int cost, int currentLevel)
    {
        this.IconGUID = IconGUID;
        this.DisplayName = displayName;
        this.MaxLevel = maxLevel;
        this.Level = currentLevel;
        this.Cost = cost;
    }
    #endregion

    public int CostFunction()
    {
        return Cost * Level;
    }
}