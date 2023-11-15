using System.Collections;
using UnityEngine;

public class GameLoopService
{
    private CoroutineRunner _coroutineRunner;
    private AsteroidFactory _asteroidFactory;
    private float _spawnPeriod;
    public GameLoopService(CoroutineRunner coroutineRunner, AsteroidFactory asteroidFactory, PlayerBaseBehaviour playerBaseBehaviour, AsteroidSettings asteroidSettings)
    {
        _coroutineRunner = coroutineRunner;
        _asteroidFactory = asteroidFactory;
        Debug.Log(_coroutineRunner);
        _coroutineRunner.StartCoroutine(GameLoop());
        _spawnPeriod = asteroidSettings.spawnPeriod;
    }

    private IEnumerator GameLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnPeriod);
            _asteroidFactory.GetFromPool();
        }
    }
}