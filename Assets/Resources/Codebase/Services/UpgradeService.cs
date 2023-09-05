using UnityEngine;
using Events;
public class UpgradeService
{
    public int DamageLevel { get; private set; }
    public int RangeLevel { get; private set; }
    public int AttackSpeedLevel { get; private set; }

    public enum UpgradeType { Damage, Range, AttackSpeed }

    public UpgraderSettings UpgraderSettings;

    private event OnDamageLevelChanged _ondamageLevelChanged;
    private event OnRangeLevelChanged _onRangeLevelChanged;
    private event OnAttackspeedLevelChanged _onAttackspeedLevelChanged;
    private event OnRangeChanged _onrangeChanged;
    private event OnUpgradeBought _onupgradeBought;
    private PlayerBaseBehaviour _playerBaseBehaviour;

    private MissileFactory _missileFactory;
   

    public UpgradeService(MissileFactory missilefactory, PlayerBaseBehaviour playerbaseBehaviour, UpgraderSettings upgraderSettings, AudioService audioService, UIService _uIService, LineRendererScript lineRenderer)
    {
        _onupgradeBought += audioService._upgradeBoughtSoundHandlers;
        _ondamageLevelChanged += _uIService._damageLevelChangedHandlers;
        _onRangeLevelChanged += _uIService._rangeLevelChangedHandlers;
        _onAttackspeedLevelChanged += _uIService._attackspeedLevelChangedHandlers;
        _onrangeChanged += lineRenderer.RangeLevelChangedHandlers;

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
                _onupgradeBought.Invoke();
                DamageLevel++;
                _missileFactory.SetDamage(UpgraderSettings.initialDamage + UpgraderSettings.damagePerGrade * DamageLevel);
                _ondamageLevelChanged?.Invoke(DamageLevel);
                break;
            case UpgradeType.Range:
                RangeLevel++;
                _onupgradeBought.Invoke();
                _playerBaseBehaviour.SetRange(UpgraderSettings.initialRange + UpgraderSettings.rangePerLevel * RangeLevel);
                _onRangeLevelChanged?.Invoke(RangeLevel);
                _onrangeChanged?.Invoke(UpgraderSettings.initialRange + UpgraderSettings.rangePerLevel * RangeLevel);
                break;
            case UpgradeType.AttackSpeed:
                AttackSpeedLevel++;
                _onupgradeBought.Invoke();
                _playerBaseBehaviour.SetAttackSpeed(UpgraderSettings.initialAttackSpeed * Mathf.Pow(UpgraderSettings.attackSpeedDecreaseMultiplier, AttackSpeedLevel));
                _onAttackspeedLevelChanged?.Invoke(AttackSpeedLevel);
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
