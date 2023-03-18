using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MissileFactoryBehaviourSettings", menuName = "ScriptableObjects/MissileFactorySettings")]

public class MissilefactorySettings : ScriptableObject
{
    [SerializeField] internal GameObject missilePrefab;
    [SerializeField] internal float missileSpeed;
}
