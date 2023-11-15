using UnityEngine;
public class UpgradeService
{
    public int DamageLevel { get; private set; }
    public int RangeLevel { get; private set; }
    public int AttackSpeedLevel { get; private set; }

    public enum UpgradeType { Damage, Range, AttackSpeed }

    public UpgraderSettings UpgraderSettings;
    private PlayerBaseBehaviour _playerBaseBehaviour;
    private LineRendererScript _lineRenderer;
    private MissileFactory _missileFactory;
    private UIService _uiService;


    public UpgradeService(MissileFactory missilefactory,
                          PlayerBaseBehaviour playerbaseBehaviour,
                          UpgraderSettings upgraderSettings,
                          LineRendererScript lineRenderer,
                          UIService uIService)
    {
        _lineRenderer = lineRenderer;
        _uiService = uIService;
        UpgraderSettings = upgraderSettings;
        _missileFactory = missilefactory;
        _playerBaseBehaviour = playerbaseBehaviour;

        _playerBaseBehaviour.SetAttackSpeed(UpgraderSettings.initialAttackSpeed);
        _playerBaseBehaviour.SetRange(UpgraderSettings.initialRange);
        _missileFactory.SetDamage(UpgraderSettings.initialDamage);
    }
    public void Upgrade(UpgradeType upgradeType)
    {
        switch (upgradeType)
        {
            case UpgradeType.Damage:
                DamageLevel++;
                _missileFactory.SetDamage(UpgraderSettings.initialDamage + UpgraderSettings.damagePerGrade * DamageLevel);
                _uiService.DrawDamageLevel(DamageLevel);
                break;
            case UpgradeType.Range:
                RangeLevel++;
                _playerBaseBehaviour.SetRange(UpgraderSettings.initialRange + UpgraderSettings.rangePerLevel * RangeLevel);
                _lineRenderer.RedrawCircle(UpgraderSettings.initialRange + UpgraderSettings.rangePerLevel * RangeLevel);
                _uiService.DrawRangeLevel(RangeLevel);
                break;
            case UpgradeType.AttackSpeed:
                AttackSpeedLevel++;
                _playerBaseBehaviour.SetAttackSpeed(UpgraderSettings.initialAttackSpeed * Mathf.Pow(UpgraderSettings.attackSpeedDecreaseMultiplier, AttackSpeedLevel));
                _uiService.DrawAttackSpeedLevel(AttackSpeedLevel);
                break;                
        }
    }
    public int GetUpgradeLevel(UpgradeType upgradeType)
    {
        switch (upgradeType)
        {
            case UpgradeType.Damage:
                return DamageLevel;
            case UpgradeType.Range:
                return RangeLevel;
            case UpgradeType.AttackSpeed:
                return AttackSpeedLevel;
        }
        Debug.LogError("unexpectable UpgradeType");
        return 0;
    }
    public float GetRange() => 
        UpgraderSettings.initialRange + UpgraderSettings.rangePerLevel * RangeLevel;
    public bool WillNotExceedCap(UpgradeType upgradeType)
    {
        switch (upgradeType)
        {
            case UpgradeType.Damage:
                return DamageLevel + 1 <= UpgraderSettings.maxAttackSpeedLevel;
                
            case UpgradeType.Range:
                return RangeLevel + 1 <= UpgraderSettings.maxRangeLevel;

            case UpgradeType.AttackSpeed:
                return AttackSpeedLevel + 1 <= UpgraderSettings.maxAttackSpeedLevel;
        }
        Debug.LogError("unexpectable UpgradeType");
        return false;
    }
}
