using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AsteroidSettings", menuName = "ScriptableObjects/AsteroidSettings")]

public class AsteroidSettings : ScriptableObject
{
    [SerializeField] internal int hp;
    [SerializeField] internal int damage;
    [SerializeField] internal float spawnRadius;
    [SerializeField] internal float asteroidSpeed;
    [SerializeField] internal float spawnPeriod;
    [SerializeField] internal float rotaionSpeed;
    [SerializeField] internal float periodDecreasePerSec;
}
