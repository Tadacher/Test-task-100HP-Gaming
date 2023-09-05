using UnityEngine;
[CreateAssetMenu(fileName = "UpgradeSettings", menuName = "ScriptableObjects/UpgradeSettings")]

public class UpgraderSettings : ScriptableObject
{
    public int initialDamage, damagePerGrade;
    public int  maxDamageLevel;

    public float initialRange, rangePerLevel;
    public int maxRangeLevel;

    public float initialAttackSpeed, attackSpeedDecreaseMultiplier;
    public int maxAttackSpeedLevel;
}
