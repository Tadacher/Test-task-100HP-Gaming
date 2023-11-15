using UnityEngine;

public class AsteroidFactory : AbstractMonoBehaviuorFactory<AsteroidBehaviour>
{
    //product dependencies
    private AsteroidSettings _asteroidSettings;
    private AudioService _audioService;
    private EconomicService _economicService;
    private Transform _target;
    //

    public override AsteroidBehaviour GetFromPool() 
        => _pool.Get();
    public AsteroidFactory(AsteroidSettings asteroidSettings, PlayerBaseBehaviour target, AudioService audioService, EconomicService economicService) : base()
    {
        _economicService = economicService;
        _audioService = audioService;
        _asteroidSettings = asteroidSettings;
        _target = target.transform;
    }
    protected override AsteroidBehaviour ConstructNew()
    {
        AsteroidBehaviour production = GameObject.Instantiate(_asteroidSettings.Prefab, RandomPosition(), Quaternion.identity, null).GetComponent<AsteroidBehaviour>();
        production.Initialize(_asteroidSettings, _target, _audioService, _economicService);

        production.SetRotaion(Random.Range(0, 2) == 0 ? 
            _asteroidSettings.rotaionSpeed : 
            -_asteroidSettings.rotaionSpeed);

        production.GetComponent<SpriteRenderer>().sprite = _asteroidSettings.Sprites[Random.Range(0, _asteroidSettings.Sprites.Length - 1)];

        return production;
    }
    protected override void GetfromPool(AsteroidBehaviour obj)
    {
          obj.transform.position = RandomPosition();
          obj.gameObject.SetActive(true);     
    }
   
    private Vector3 RandomPosition() 
        => Random.insideUnitCircle.normalized * _asteroidSettings.spawnRadius;

}
