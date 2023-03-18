using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EconomicsManagerSettings", menuName = "ScriptableObjects/EconomicsManagerSettings")]

public class EconomicsManagerSettings : ScriptableObject
{
    [SerializeField] internal int incomePerAsteroid;
    [SerializeField] internal int initialCost;
    [SerializeField] internal float costMultiplier;
}
