using UnityEngine;
using Zenject;

public class PlayerBaseBehaviour : MonoBehaviour
{
    private float launchPeriod;
    private float range;
    private float currentTimer;
    private int hitPoints;
    private Collider2D[] availableTargets;


    private MissileFactory missileFactory;
    public MissileFactory MissileFactory
    {
        get => missileFactory;
        set
        {
            if (missileFactory == null) missileFactory = value;
            else ConsoleShortCuts.FieldOverrideWarn("MissileFactory");
        }
    }
    public Events.PlayerDeath OnPlayerDeath;


    [Inject]
    public void Initialize(PlayerBehaviourSettings _playerBehaviourSettings)
    {
        hitPoints = _playerBehaviourSettings.hitPoints;
    }


    private void Update()
    {
        currentTimer -= Time.deltaTime;

        if (currentTimer > 0) return;


        Transform nearestTarget = GetNearestTarget();

        if (nearestTarget != null)
        {
            currentTimer = launchPeriod;
            LaunchMisile(nearestTarget);
        }
        else
            currentTimer = 0.2f;
    }

    public void RecieveDamage(int damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0) OnPlayerDeath?.Invoke();
    }

    public void LaunchMisile(Transform target) => MissileFactory.GetFromPool().SetTarget(target);
    public void SetRange(float _range) => range = _range;
    public void SetAttackSpeed(float period) => launchPeriod = period;

    private Transform GetNearestTarget()
    {
        availableTargets = Physics2D.OverlapCircleAll(Vector2.zero, range, LayerMask.GetMask("Asteroid"));
        Transform nearestTarget = null;

        if (availableTargets.Length == 0)
            return null;

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
}
