using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

[RequireComponent(typeof(Rigidbody), typeof(CharacterAnimationController), typeof(AudioSource))]
public class CharacterController : MonoBehaviour
{
    [SerializeField] private Hand _hand;
    [SerializeField] public float _speed;
    [SerializeField] public float _health = 50;
    [SerializeField] public int _lvl = 0;
    [SerializeField] private float _coins = 1;

    private bool isDead = false;
    private Rigidbody _rb;
    private CharacterAnimationController _animController;
    private CharacterUIController _ui;
    private WeaponPrefabs _weaponPrefabs;
    public float _curHealth;
    public Action onLVLUp;
    public Action onDeath;
    public Action<int> onCoinChanged;
    public Transform _target;
    public string _nick;
    private LeaderboardUI _leaderboard;
    [HideInInspector] public AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _weaponPrefabs = FindObjectOfType<WeaponPrefabs>();
        _leaderboard = FindObjectOfType<LeaderboardUI>();
        _rb = GetComponent<Rigidbody>();
        _animController = GetComponent<CharacterAnimationController>();
        _ui = GetComponent<CharacterUIController>();
        _curHealth = _health;
        FindObjectOfType<SettingsController>().onUpdate += UpdateVolume;
        UpdateVolume();
    }

    private void Start()
    {
        _ui.SetHP(_curHealth / _health);
        CheckCoins();
    }

    private void Update()
    {
        FindClosestEnemy();
    }

    private void UpdateVolume()
    {
        if (_audioSource != null) 
            _audioSource.volume = YandexGame.savesData.soundValue;
    }

    private void FindClosestEnemy()
    {
        _target = null;
        float distance = Mathf.Infinity;
        foreach (Collider go in Physics.OverlapSphere(transform.position, 10, 1 << 6))
        {
            if (go.GetComponent<CharacterController>().isDead || go.GetComponent<CharacterController>() == this)
                continue;
            float curDistance = (go.transform.position - transform.position).sqrMagnitude;
            if (curDistance < distance)
            {
                _target = go.transform;
                distance = curDistance;
            }
        }
        if (_target != null)
            Rotate(Vector2.one);
    }

    public void AddHealth(int health)
    {
        _curHealth = Mathf.Min(_health, _curHealth + health);
        _ui.SetHP(_curHealth / _health);
    }

    public void Move(Vector2 direction)
    {
        if (isDead) 
            return;
        if (direction == Vector2.zero)
        {
            _animController.Running(false);
            return;
        }
        _animController.Running(true);
        _rb.MovePosition(_rb.position + new Vector3(direction.x, 0, direction.y) * Time.deltaTime * _speed);
        Rotate(direction);
    }

    public void Rotate(Vector2 direction)
    {
        if (isDead || direction == Vector2.zero)
            return;
        if (_target != null)
            direction = (new Vector2(_target.position.x, _target.position.z) - new Vector2(_rb.position.x, _rb.position.z)).normalized;
        transform.rotation = Quaternion.Euler(0, Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg, 0);
    }

    public void Shoot() 
    {
        if (isDead)
            return;
        _hand.Shoot();
    }

    public void TakeDamage(CharacterController from, float damage)
    {
        if (from == this || isDead)
            return;
        _curHealth -= damage;
        if (_curHealth <= 0)
        {
            _curHealth = 0;
            from.AddCoins((int)MathF.Pow(2, _lvl));
            Death();
        }
        else
            _animController.Hit();
        _ui.SetHP(_curHealth / _health);
    }

    public void AddCoins(int coins)
    {
        _coins += coins;
        CheckCoins();
    }

    public void Stop()
    {
        _animController.Fail();
        isDead = true;
        _hand.gameObject.SetActive(false);
        _rb.isKinematic = true;
        GetComponent<Collider>().enabled = false;
        onDeath?.Invoke();
    }

    private void Death()
    {
        _animController.Death();
        isDead = true;
        _hand.gameObject.SetActive(false);
        _rb.isKinematic = true;
        GetComponent<Collider>().enabled = false;
        onDeath?.Invoke();
        if (TryGetComponent(out EnemyController e) || SceneManager.GetActiveScene().buildIndex == 1)
            Destroy(gameObject, 5);
        else
            Invoke("Respawn", 5);
    }

    private void Respawn()
    {
        _animController.Respawn();
        isDead = false;
        _hand.gameObject.SetActive(true);
        _hand.PrepareWeapon();
        _rb.isKinematic = false;
        GetComponent<Collider>().enabled = true;
        _curHealth = _health;
        _ui.SetHP(_curHealth / _health);
    }

    public void SetNick(string nick)
    {
        _nick = nick;
    }

    public void SetWeapon(int id)
    {
        _hand.weapon = _weaponPrefabs.GetWeapon(id).GetComponent<Weapon>();
        if (id == 0)
            _hand.SpawnBow();
        else
            _hand.DespawnBow();
        _hand.PrepareWeapon();
    }

    public void SetHero(GameObject hero)
    {
        Instantiate(hero, transform);
        _animController.UpdateAnimator();
    }

    public void SetSkin(int skin)
    {
        GetComponentInChildren<CharacterModel>().ChangeSkin(skin);
    }

    public void CheckCoins()
    {
        onCoinChanged?.Invoke(Mathf.Min(100, (int)(_coins / Mathf.Pow(2, _lvl) * 100)));
        if (_lvl == 10 || Mathf.Pow(2, _lvl) > _coins)
            return;
        _coins -= Mathf.Pow(2, _lvl);
        _lvl++;
        _leaderboard.UpdateLeaderboard();
        _ui.SetLVL(_lvl);
        onLVLUp?.Invoke();
    }
}
