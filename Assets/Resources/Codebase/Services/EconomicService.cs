using System;
using Unity.VisualScripting;
using UnityEngine;

public class EconomicService
{
    private EconomicsManagerSettings _economicsManagerSettings;
    private UpgradeService _upgradeService;
    private GameplayUIService _uIService;
 
    public int balance;
    public EconomicService(UpgradeService upgradeManager, EconomicsManagerSettings economicsManagerSettings, GameplayUIService UiService, CoreGameplayUiContainer canvasRefsContainer)
    {
        canvasRefsContainer.damageUpgrade.onClick.AddListener(() => BuyUpgrade(UpgradeService.UpgradeType.Damage));
        canvasRefsContainer.rangeUpgrade.onClick.AddListener(() => BuyUpgrade(UpgradeService.UpgradeType.Range));
        canvasRefsContainer.attackSpeedUpgrade.onClick.AddListener(() => BuyUpgrade(UpgradeService.UpgradeType.AttackSpeed));
        _economicsManagerSettings = economicsManagerSettings;
        _upgradeService = upgradeManager;
        _uIService = UiService;
    }

    public void CountIncome()
    {
        balance += _economicsManagerSettings.incomePerAsteroid;
        _uIService.DrawFunds(balance);
        RedrawAcessibility();
    }
    public void DecreaseBalance(int ammount)
    {
        balance -= ammount;
    }
    public int GetUpgradeCost(int level) =>
        _economicsManagerSettings.initialCost * (int)Mathf.Pow(_economicsManagerSettings.costMultiplier, level + 1);
    public void BuyUpgrade(UpgradeService.UpgradeType upgradeType)
    {
        if (balance < GetUpgradeCost(_upgradeService.GetUpgradeLevel(upgradeType)))
            return;

        DecreaseBalance(GetUpgradeCost(_upgradeService.GetUpgradeLevel(upgradeType)));
        _upgradeService.Upgrade(upgradeType);
        Debug.Log("lol");
        RedrawAcessibility();
    }

    private void RedrawAcessibility()
    {
        _uIService.SetAttackSpeedUpgradeAvailability(CheckForAvailability(UpgradeService.UpgradeType.AttackSpeed));
        _uIService.SetDamageUpgradeAvailability(CheckForAvailability(UpgradeService.UpgradeType.Damage));
        _uIService.SetRangeUpgradeAvailability(CheckForAvailability(UpgradeService.UpgradeType.Range));
    }

    public bool CheckForAvailability(UpgradeService.UpgradeType upgradeType) =>
        balance >= GetUpgradeCost(_upgradeService.GetUpgradeLevel(upgradeType)) && _upgradeService.WillNotExceedCap(upgradeType);
}
