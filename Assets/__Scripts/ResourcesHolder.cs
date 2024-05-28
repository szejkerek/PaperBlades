using System;
using Unity.VisualScripting;

[Serializable]
public class ResourcesHolder
{
    public static Action<ResourcesHolder> OnResourcesUpdated;
    public int Money {  get; private set; }
    public int Expirience {  get; private set; }

    public void AddMoney(int money)
    {
        Money += money;
        OnResourcesUpdated?.Invoke(this);
    }

    public bool TryRemoveMoney(int money)
    {
        if (Money - money < 0) 
            return false; 

        Money -= money;
        OnResourcesUpdated?.Invoke(this);
        return true;
    }
    public void AddExpirience(int exp)
    {
        Expirience += exp;
        OnResourcesUpdated?.Invoke(this);
    }

    internal void AddMoney(object calculateMoney)
    {
        throw new NotImplementedException();
    }
}
