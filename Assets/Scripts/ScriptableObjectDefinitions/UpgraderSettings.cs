using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "UpgradeSettings", menuName = "ScriptableObjects/UpgradeSettings")]

public class UpgraderSettings : ScriptableObject
{
    [SerializeField] internal int initialDamage, damagePerGrade;
    [SerializeField] internal int  maxDamageLevel;

    [SerializeField] internal float initialRange, rangePerLevel;
    [SerializeField] internal int maxRangeLevel;

    [SerializeField] internal float initialAttackSpeed, attackSpeedDecreaseMultiplier;
    [SerializeField] internal int maxAttackSpeedLevel;
}
