using UnityEngine;

namespace Events
{
    internal delegate void AsteroidDeath();

    internal delegate void BalanceChanged(int balance);
    internal delegate void DamageLevelChanged(int level);
    internal delegate void RangeLevelChanged(int level);
    internal delegate void AttackspeedLevelChanged(int level);
    internal delegate void RangeChanged(float newRange);
    internal delegate void UpgradeBought();
    internal delegate void MissileLaunched();
    internal delegate void MissileDestroyed(Transform missileTransform);


    internal delegate void GameStateChanged(GameStateMachine.GameStates nextGameState);
    internal delegate void Pause();
    internal delegate void Play();
    internal delegate void PlayerDeath();
}
