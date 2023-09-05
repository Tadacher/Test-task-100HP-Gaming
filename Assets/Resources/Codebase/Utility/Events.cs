using UnityEngine;

namespace Events
{
    public delegate void OnAsteroidDeath();

    public delegate void OnBalanceChanged(int balance);
    public delegate void OnDamageLevelChanged(int level);
    public delegate void OnRangeLevelChanged(int level);
    public delegate void OnAttackspeedLevelChanged(int level);
    public delegate void OnRangeChanged(float newRange);
    public delegate void OnUpgradeBought();
    public delegate void OnMissileLaunched();
    public delegate void OnMissileDestroyed(Transform missileTransform);


    public delegate void GameStateChanged(GameStateMachine.GameStates nextGameState);
    public delegate void Pause();
    public delegate void Play();
    public delegate void PlayerDeath();
}
