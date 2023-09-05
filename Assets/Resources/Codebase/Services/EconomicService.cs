using UnityEngine;
using Events;

public class EconomicService
{
    private EconomicsManagerSettings _economicsManagerSettings;

    private UpgradeService _upgradeManager;
    public UpgradeService UpgradeManager
    {
        get => _upgradeManager;
        set
        {
            if (_upgradeManager == null) 
                _upgradeManager = value;
            else
                ConsoleShortCuts.FieldOverrideWarn("upgradeManager");
        }
    }


    public int balance;

    public event OnBalanceChanged OnBalanceChanged;
    public event OnUpgradeBought OnUpgradeBought;
    public EconomicService(UpgradeService upgradeManager, EconomicsManagerSettings economicsManagerSettings)
    {
        _economicsManagerSettings = economicsManagerSettings;
        _upgradeManager = upgradeManager;
    }

    public void CountIncome()
    {
        balance += _economicsManagerSettings.incomePerAsteroid;
        OnBalanceChanged?.Invoke(balance);
    }
    public void DecreaseBalance(int ammount)
    {
        balance -= ammount;
        OnBalanceChanged?.Invoke(balance);
    }
    public int GetUpgradeCost(int level) =>
        _economicsManagerSettings.initialCost * (int)Mathf.Pow(_economicsManagerSettings.costMultiplier, level + 1);
    public void BuyUpgrade(UpgradeService.UpgradeType upgradeType)
    {
        if (balance < GetUpgradeCost(_upgradeManager.GetUpgradeLevel(upgradeType)))
            return;

        DecreaseBalance(GetUpgradeCost(_upgradeManager.GetUpgradeLevel(upgradeType)));
        UpgradeManager.Upgrade(upgradeType);
        OnUpgradeBought?.Invoke();
    }
    public bool CheckForAvailability(UpgradeService.UpgradeType upgradeType) =>
        balance >= GetUpgradeCost(_upgradeManager.GetUpgradeLevel(upgradeType)) && _upgradeManager.WillNotExceedCap(upgradeType);
}
