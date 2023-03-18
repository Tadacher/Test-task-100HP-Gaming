using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class AsteroidFactory : MonoBehaviour
{
    AsteroidSettings asteroidSettings;

    float currentTimePast;
    float spawnPeriod;
    internal AsteroidDeath paidAsteroidDeathHandlers;
    internal AsteroidDeath unpaidAsteroidDeathHandlers;

    [SerializeField] Sprite[] asteroidSprites;
    [SerializeField] GameObject asteroidBase;

    GameObject constructionField;
    Transform target;
    public Transform Target
    {
        private get 
        { 
            return target; 
        }
        set
        {
            if (target == null) target = value;
            else Debug.LogWarning("Multiple attemts to set player position!");
        }
    }
    internal void Initialize(AsteroidSettings _asteroidSettings)
    {
        asteroidSettings = _asteroidSettings;
        spawnPeriod = _asteroidSettings.spawnPeriod;
    }

    // Update is called once per frame
    void Update()
    {
        currentTimePast += Time.deltaTime;
        if(currentTimePast> spawnPeriod)
        {
            currentTimePast = 0;
            CounstructAsteroid();
        }
        spawnPeriod -= Time.deltaTime * asteroidSettings.periodDecreasePerSec;
    }
    void CounstructAsteroid()
    {
        constructionField = Instantiate(asteroidBase, RandomPosition(), Quaternion.identity, null);

        constructionField.TryGetComponent<AsteroidBehaviour>(out AsteroidBehaviour constructedAsteroidBehaviour);
        constructedAsteroidBehaviour.speed = asteroidSettings.asteroidSpeed;
        constructedAsteroidBehaviour.damage = asteroidSettings.damage;
        constructedAsteroidBehaviour.PlayerBase = target;
        constructedAsteroidBehaviour.OnPaidDeath += paidAsteroidDeathHandlers;
        constructedAsteroidBehaviour.OnUnpaidEvent += unpaidAsteroidDeathHandlers;
        constructedAsteroidBehaviour.hitPonts = asteroidSettings.hp;
        constructedAsteroidBehaviour.rotationSpeed = Random.Range(0, 2) == 0 ? asteroidSettings.rotaionSpeed : -asteroidSettings.rotaionSpeed;
        constructionField.TryGetComponent<SpriteRenderer>(out SpriteRenderer constructedAsteroidSpriteRenderer);
        constructedAsteroidSpriteRenderer.sprite = asteroidSprites[Random.Range(0, asteroidSprites.Length - 1)];
    }
    Vector3 RandomPosition()
    {
        return Random.insideUnitCircle.normalized * asteroidSettings.spawnRadius;
    }
   
}
