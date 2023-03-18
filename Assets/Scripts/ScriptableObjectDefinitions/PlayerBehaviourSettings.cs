using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerBehaviourSettings", menuName = "ScriptableObjects/PlayerBehaviourSettings")]

public class PlayerBehaviourSettings : ScriptableObject
{
    [SerializeField] internal int hitPoints;
}
