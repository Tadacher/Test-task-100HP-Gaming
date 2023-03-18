using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseBehaviour : MonoBehaviour
{
    float launchPeriod;
    float range;
    float currentTimer;
    int hitPoints;
    Collider2D[] availableTargets;

    PlayerBehaviourSettings playerBehaviourSettings;

    MissileFactory missileFactory;
    internal MissileFactory MissileFactory
    {
        get { return missileFactory; }
        set 
        {
            if (missileFactory == null) missileFactory = value;
            else ConsoleShortCuts.FieldOverrideWarn("MissileFactory");
        }
    }
    internal Events.PlayerDeath OnPlayerDeath;
    internal void Initialize(PlayerBehaviourSettings _playerBehaviourSettings)
    {
        hitPoints = _playerBehaviourSettings.hitPoints;
        playerBehaviourSettings = _playerBehaviourSettings;
    }
    private void Update()
    {
        currentTimer -= Time.deltaTime;
        if(currentTimer<=0)
        {         
            Transform nearestTarget = GetNearestTarget();
            if (nearestTarget != null)
            {
                currentTimer = launchPeriod;
                LaunchMisile(nearestTarget);
            }
            else
            {
                currentTimer = 0.2f;
            }
        }
    }

    internal void RecieveDamage(int damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0) OnPlayerDeath?.Invoke();
    }
    void LaunchMisile(Transform target)
    {
        MissileFactory.ConstructMissile(target);
    }
    internal void SetRange(float _range) 
    {
        range = _range;
    }
    internal void SetAttackSpeed(float period)
    {
        launchPeriod = period;
    }
    Transform GetNearestTarget()
    {
        availableTargets = Physics2D.OverlapCircleAll(Vector2.zero, range, LayerMask.GetMask("Asteroid"));
        Transform nearestTarget = null;
        if (availableTargets.Length > 0)
        {
            float nearestDistance = Mathf.Infinity;
            foreach (Collider2D collider in availableTargets)
            {
                if ((collider.transform.position - transform.position).magnitude < nearestDistance)
                {
                    nearestDistance = (collider.transform.position - transform.position).magnitude;
                    nearestTarget = collider.transform;
                }
            }
            return nearestTarget;
        }
        else return null;     
    }
}
