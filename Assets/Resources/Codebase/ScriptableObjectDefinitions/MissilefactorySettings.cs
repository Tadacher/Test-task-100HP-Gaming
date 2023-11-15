using UnityEngine;
[CreateAssetMenu(fileName = "MissileFactoryBehaviourSettings", menuName = "ScriptableObjects/MissileFactorySettings")]

public class MissilefactorySettings : ScriptableObject
{
    public MissileBehaviour missilePrefab;
    public float missileSpeed;
}
