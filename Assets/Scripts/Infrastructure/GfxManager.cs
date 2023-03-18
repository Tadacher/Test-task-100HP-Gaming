using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GfxManager : MonoBehaviour
{

    [SerializeField] GameObject ExplosionPrefab;
    internal Events.MissileDestroyed missileDestroyedGfxHandlers;
    internal void Initialize()
    {
        missileDestroyedGfxHandlers += SpawnExplosion;
    }
    void SpawnExplosion(Transform target)
    {
        Instantiate(ExplosionPrefab, target.position, target.rotation);
    }
}
