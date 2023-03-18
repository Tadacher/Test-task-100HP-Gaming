using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GlobalSettings", menuName = "ScriptableObjects/GlobalSettings")]
public class GlobalSettings : ScriptableObject
{
    [SerializeField] internal UpgraderSettings upgraderSettings;
    [SerializeField] internal AsteroidSettings asteroidSettings;
    [SerializeField] internal PlayerBehaviourSettings playerBehaviourSettings;
    [SerializeField] internal MissilefactorySettings missilefactorySettings;
    [SerializeField] internal EconomicsManagerSettings economicsManagerSettings;

}
