using Cinemachine;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cvc;
    [SerializeField] private EnemySpawner _enemySpawner;
    [Space]
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _playerHero;
    [SerializeField] private int _playerSkin;
    [SerializeField] private int _playerWeapon;

    private HeroPrefabs _heroPrefabs;
    private WeaponPrefabs _weaponPrefabs;
    private CharacterController _player;

    private void Awake()
    {
        _heroPrefabs = FindObjectOfType<HeroPrefabs>();
        _weaponPrefabs = FindObjectOfType<WeaponPrefabs>();
        _player = Instantiate(_playerPrefab).GetComponent<CharacterController>();
        _player.SetHero(_heroPrefabs.GetHero(_playerHero));
        _player.SetSkin(_playerSkin);
        _player.SetWeapon(_playerWeapon);
        _cvc.Follow = _player.gameObject.transform;

        for (int i = 0; i < _enemySpawner._enemyCount; ++i)
            SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        Vector3 position = _enemySpawner.GetPos();
        CharacterController _enemy = Instantiate(_enemyPrefab, position, _enemyPrefab.transform.rotation).GetComponent<CharacterController>();
        _enemy.SetHero(_heroPrefabs.GetHero(Random.Range(0, _heroPrefabs.GetHeroesLength())));
        _enemy.SetSkin(Random.Range(0, 5));
        _enemy.SetWeapon(Random.Range(0, _weaponPrefabs.GetWeaponsLength()));
        _enemy.GetComponent<EnemyController>().SetLVL(_player._lvl + 1);
        _enemy.onDeath += SpawnEnemy;
    }

    public Vector3 GetPlayerPos()
    {
        return _player.transform.position;
    }
}
