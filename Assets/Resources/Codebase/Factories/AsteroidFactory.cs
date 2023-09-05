
using UnityEngine;
using Events;
using System.Collections;

public class AsteroidFactory : AbstractMonoBehaviuorFactory<AsteroidBehaviour>
{
    public OnAsteroidDeath PaidAsteroidDeathHandlers { get; set; }
    public OnAsteroidDeath UnpaidAsteroidDeathHandlers { get; set; }

    private OnAsteroidDeath paidAsteroidDeathHandlers;
    private OnAsteroidDeath unpaidAsteroidDeathHandlers;

    private AsteroidSettings _asteroidSettings;
   

    private Sprite[] _asteroidSprites;
    private GameObject _asteroidBase;
    private GameObject _constructionField;
    private Transform _target;
    
    private CoroutineRunner _coroutineRunner;
    private float _spawnPeriod;
       
    public Transform Target
    {
        private get => _target;
        set
        {
            if (_target == null) 
                _target = value;
            else 
                Debug.LogWarning("Multiple attemts to set player position!");
        }
    }

    public override AsteroidBehaviour GetFromPool() => _pool.Get();

    public AsteroidFactory(AsteroidSettings asteroidSettings, CoroutineRunner coroutineRunner, PlayerBaseBehaviour target, AudioService audioService, EconomicService economicService) : base()
    {
        paidAsteroidDeathHandlers += audioService._asteroidDeathSoundHandlers;
        unpaidAsteroidDeathHandlers += audioService._asteroidDeathSoundHandlers;
        PaidAsteroidDeathHandlers += economicService.CountIncome;
        _asteroidSettings = asteroidSettings;
        _spawnPeriod = _asteroidSettings.spawnPeriod;
        _coroutineRunner = coroutineRunner;
        _target = target.transform;
    }
    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            GetFromPool();
            yield return new WaitForSeconds(_spawnPeriod);
        }
    }


    protected override AsteroidBehaviour ConstructNew()
    {
        _constructionField = GameObject.Instantiate(_asteroidBase, RandomPosition(), Quaternion.identity, null);

        AsteroidBehaviour constructedAsteroidBehaviour = _constructionField.GetComponent<AsteroidBehaviour>();
        constructedAsteroidBehaviour.speed = _asteroidSettings.asteroidSpeed;
        constructedAsteroidBehaviour.damage = _asteroidSettings.damage;
        constructedAsteroidBehaviour.PlayerBase = _target;
        constructedAsteroidBehaviour.OnPaidDeath += PaidAsteroidDeathHandlers;
        constructedAsteroidBehaviour.OnUnpaidEvent += UnpaidAsteroidDeathHandlers;
        constructedAsteroidBehaviour.hitPonts = _asteroidSettings.hp;
        constructedAsteroidBehaviour.rotationSpeed = Random.Range(0, 2) == 0 ? _asteroidSettings.rotaionSpeed : -_asteroidSettings.rotaionSpeed;
        _constructionField.TryGetComponent<SpriteRenderer>(out SpriteRenderer constructedAsteroidSpriteRenderer);
        constructedAsteroidSpriteRenderer.sprite = _asteroidSprites[Random.Range(0, _asteroidSprites.Length - 1)];
        return constructedAsteroidBehaviour;
    }
   
    Vector3 RandomPosition() => 
        Random.insideUnitCircle.normalized * _asteroidSettings.spawnRadius;

    protected override void GetfromPool(AsteroidBehaviour obj)
    {
          obj.transform.position = RandomPosition();
          obj.gameObject.SetActive(true);     
    }
}
