using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour
{
    private const string _playerTag = "Player";
    private float _rotationSpeed;
    private int _hitPonts;
    private int _damage;
    private float _speed;

    private Transform _target;
    private AudioService _audioService;
    private EconomicService _economicService;
    public void Initialize(AsteroidSettings asteroidSettings, Transform target, AudioService audioService, EconomicService economicService)
    {
        _audioService = audioService;
        _economicService = economicService;

        _rotationSpeed = asteroidSettings.rotaionSpeed;
        _hitPonts = asteroidSettings.hp;
        _damage = asteroidSettings.damage;
        _speed = asteroidSettings.asteroidSpeed;
        _target = target;
    }

    void Update()
    {
        Move();
        Rotate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag is not _playerTag)
            return;

        collision.gameObject.GetComponent<PlayerBaseBehaviour>().RecieveDamage(_damage);
        UnpaidDeath();
    }

    public void RecieveDamage(int damage)
    {
        _hitPonts -= damage;

        if (_hitPonts <= 0) 
            PaidDeath();
    }
    public void SetRotaion(float rotationSpeed) 
        => _rotationSpeed = rotationSpeed;
    private void Rotate()
        => transform.eulerAngles += _rotationSpeed * Time.deltaTime * Vector3.forward;
    private void Move() 
        => transform.Translate(_speed * Time.deltaTime * (_target.position - transform.position).normalized, Space.World);

    private void UnpaidDeath()
    {
        PlayDeathSound();
        Destroy(gameObject);
    }
    private void PaidDeath()
    {
        _economicService.CountIncome();
        PlayDeathSound();
        Destroy(gameObject);
    }
    private void PlayDeathSound()
        => _audioService.PlayAsteroidDeathSound();

}
