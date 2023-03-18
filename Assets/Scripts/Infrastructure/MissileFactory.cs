using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class MissileFactory : MonoBehaviour
{
    GameObject missilePrefab;
    float speed;
     int damage;
    MissileBehaviour missileBehaviour;
    internal event MissileLaunched OnMissileLaunched;
    internal MissileDestroyed missileDestroyedHandlers;
    internal void Initialize(MissilefactorySettings missilefactorySettings)
    {
        missilePrefab = missilefactorySettings.missilePrefab;
        speed = missilefactorySettings.missileSpeed;
    }
    internal GameObject ConstructMissile (Transform target)
    {     
        missileBehaviour = Instantiate(missilePrefab, null).GetComponent<MissileBehaviour>();
        missileBehaviour.Initialize(speed, damage, target);
        missileBehaviour.OnMissileDestroyed += missileDestroyedHandlers;
        OnMissileLaunched?.Invoke();
        return missileBehaviour.gameObject;
    }
    internal void SetDamage(int _damage)
    {
        damage = _damage;
    }
}
