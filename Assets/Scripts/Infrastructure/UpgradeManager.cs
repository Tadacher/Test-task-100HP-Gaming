using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
public class UpgradeManager : MonoBehaviour
{
    internal int DamageLevel { get; private set; }
    internal int RangeLevel { get; private set; }
    internal int AttackSpeedLevel { get; private set; }
    
    internal enum UpgradeType { Damage, Range, AttackSpeed }

    internal UpgraderSettings upgraderSettings;

    internal event DamageLevelChanged OndamageLevelChanged;
    internal event RangeLevelChanged OnRangeLevelChanged;
    internal event AttackspeedLevelChanged OnAttackspeedLevelChanged;
    internal event RangeChanged OnrangeChanged;

    PlayerBaseBehaviour playerBaseBehaviour;
    PlayerBaseBehaviour PlayerBaseBehaviour
    {
        get { return playerBaseBehaviour; }
        set
        {
            if (playerBaseBehaviour == null) playerBaseBehaviour = value;
            else ConsoleShortCuts.FieldOverrideWarn("playerBaseBehaviour");
        }
    }

    MissileFactory missileFactory;
    MissileFactory MissileFactory
    {
        get { return missileFactory; }
        set
        {
            if (missileFactory == null) missileFactory = value;
            else ConsoleShortCuts.FieldOverrideWarn("missileFactory");
        }
    }
    
    internal void Initialize(MissileFactory _missilefactory, PlayerBaseBehaviour _playerbaseBehaviour, UpgraderSettings _upgraderSettings)
    {
        upgraderSettings = _upgraderSettings;
        missileFactory = _missilefactory;
        playerBaseBehaviour = _playerbaseBehaviour;
        playerBaseBehaviour.SetAttackSpeed(upgraderSettings.initialAttackSpeed);
        playerBaseBehaviour.SetRange(upgraderSettings.initialRange);
        missileFactory.SetDamage(upgraderSettings.initialDamage);
    }
    internal void Upgrade(UpgradeType upgradeType)
    {
        switch (upgradeType)
        {
            case UpgradeType.Damage:
                DamageLevel++;
                missileFactory.SetDamage(upgraderSettings.initialDamage + upgraderSettings.damagePerGrade * DamageLevel);
                OndamageLevelChanged?.Invoke(DamageLevel);
                break;
            case UpgradeType.Range:
                RangeLevel++;
                playerBaseBehaviour.SetRange(upgraderSettings.initialRange + upgraderSettings.rangePerLevel * RangeLevel);
                OnRangeLevelChanged?.Invoke(RangeLevel);
                OnrangeChanged?.Invoke(upgraderSettings.initialRange + upgraderSettings.rangePerLevel * RangeLevel);
                break;
            case UpgradeType.AttackSpeed:
                AttackSpeedLevel++;
                playerBaseBehaviour.SetAttackSpeed(upgraderSettings.initialAttackSpeed * Mathf.Pow(upgraderSettings.attackSpeedDecreaseMultiplier, AttackSpeedLevel));
                OnAttackspeedLevelChanged?.Invoke(AttackSpeedLevel);
                break;                
        }
    }
    internal int GetUpgradeLevel(UpgradeType upgradeType)
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
    internal float GetRange()
    {
        return upgraderSettings.initialRange + upgraderSettings.rangePerLevel * RangeLevel;
    }
    internal bool WillNotExceedCap(UpgradeType upgradeType)
    {
        switch (upgradeType)
        {

            case UpgradeType.Damage:
                if (DamageLevel + 1 <= upgraderSettings.maxAttackSpeedLevel) return true;
                else return false;
            case UpgradeType.Range:
                if (RangeLevel + 1 <= upgraderSettings.maxRangeLevel) return true;
                else return false;
                    case UpgradeType.AttackSpeed:
                if (AttackSpeedLevel + 1 <= upgraderSettings.maxAttackSpeedLevel) return true;
                else return false;
                    }
        Debug.LogError("unexpectable UpgradeType");
        return false;
    }
}
