using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class EconomicsManager : MonoBehaviour
{
    EconomicsManagerSettings economicsManagerSettings;

    UpgradeManager upgradeManager;
    UpgradeManager UpgradeManager
    {
        get { return upgradeManager; }
        set 
        {
            if (upgradeManager == null) upgradeManager = value;
            else ConsoleShortCuts.FieldOverrideWarn("upgradeManager");
        }
    }
    
    
    internal int balance;

    internal event BalanceChanged OnBalanceChanged;
    internal event UpgradeBought OnUpgradeBought;
    internal void Initialize(UpgradeManager _upgradeManager, EconomicsManagerSettings _economicsManagerSettings)
    {
        economicsManagerSettings = _economicsManagerSettings;
        upgradeManager = _upgradeManager;
        if (upgradeManager == null) ConsoleShortCuts.NotInjectedWarn("upgradeManager");
    }

    internal void CountIncome()
    {
        balance += economicsManagerSettings.incomePerAsteroid;
        OnBalanceChanged?.Invoke(balance);
    }
    internal void DecreaseBalance(int ammount)
    {
        balance -= ammount;
        OnBalanceChanged?.Invoke(balance);
    }
    internal int GetUpgradeCost(int level)
    {
        return economicsManagerSettings.initialCost * (int)Mathf.Pow(economicsManagerSettings.costMultiplier, level+1);
    }
    internal void BuyUpgrade(UpgradeManager.UpgradeType upgradeType)
    {
        if(balance>= GetUpgradeCost(upgradeManager.GetUpgradeLevel(upgradeType)))
        {
            DecreaseBalance(GetUpgradeCost(upgradeManager.GetUpgradeLevel(upgradeType)));
            UpgradeManager.Upgrade(upgradeType);
            OnUpgradeBought?.Invoke();
        }
    }
    internal bool CheckForAvailability(UpgradeManager.UpgradeType upgradeType)
    {
        Debug.Log(GetUpgradeCost(upgradeManager.GetUpgradeLevel(upgradeType)));
        return balance >= GetUpgradeCost(upgradeManager.GetUpgradeLevel(upgradeType)) && upgradeManager.WillNotExceedCap(upgradeType);
    }
}
