using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AsteroidSettings", menuName = "ScriptableObjects/AsteroidSettings")]

public class AsteroidSettings : ScriptableObject
{
    public int hp;
    public int damage;
    public float spawnRadius;
    public float asteroidSpeed;
    public float spawnPeriod;
    public float rotaionSpeed;
    public float periodDecreasePerSec;
    public AsteroidBehaviour Prefab;
    public Sprite[] Sprites;
}
