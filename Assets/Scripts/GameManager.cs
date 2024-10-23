using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cvc;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private GameUIController _gameUI;
    [SerializeField] private LeaderboardUI _leaderboard;
    [Space]
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _playerHero;
    [SerializeField] private int _playerSkin;
    [SerializeField] private int _playerWeapon;

    private HeroPrefabs _heroPrefabs;
    private WeaponPrefabs _weaponPrefabs;
    private CharacterControllerMy _player;

    private bool _isGameStopped = false;

    private void Awake()
    {
        _playerHero = PlayerPrefs.GetInt("hero", 4);
        _playerSkin = PlayerPrefs.GetInt("skin", 0);
        _playerWeapon = PlayerPrefs.GetInt("weapon", 0);

        _heroPrefabs = FindObjectOfType<HeroPrefabs>();
        _weaponPrefabs = FindObjectOfType<WeaponPrefabs>();
        _player = Instantiate(_playerPrefab).GetComponent<CharacterControllerMy>();
        _player.SetHero(_heroPrefabs.GetHero(_playerHero));
        _player.SetSkin(_playerSkin);
        _player.SetWeapon(_playerWeapon);
        _cvc.Follow = _player.gameObject.transform;
        _player.onLVLUp += _leaderboard.UpdateLeaderboard;

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            _player.onDeath += () => _gameUI.EndPanel("Great");
            _player.onDeath += Stop;
            _player.onDeath += _leaderboard.UpdateLeaderboard;
        }
        else
        {
            _gameUI.SetTimer();
            _gameUI.onTimerEnd += () => _gameUI.EndPanel("Time's up");
            _gameUI.onTimerEnd += StopCharacters;
        }

        for (int i = 0; i < _enemySpawner._enemyCount; ++i)
            SpawnEnemy();
        _leaderboard.UpdateLeaderboard();
    }

    private void StopCharacters()
    {
        _isGameStopped = true;
        foreach (CharacterControllerMy character in FindObjectsOfType<CharacterControllerMy>())
            character.Stop();
    }
    public void SpawnEnemy()
    {
        if (_isGameStopped)
            return;
        Vector3 position = _enemySpawner.GetPos();
        CharacterControllerMy _enemy = Instantiate(_enemyPrefab, position, _enemyPrefab.transform.rotation).GetComponent<CharacterControllerMy>();
        _enemy.SetHero(_heroPrefabs.GetHero(Random.Range(0, _heroPrefabs.GetHeroesLength())));
        _enemy.SetSkin(Random.Range(0, 5));
        _enemy.SetWeapon(Random.Range(0, _weaponPrefabs.GetWeaponsLength()));
        _enemy.GetComponent<EnemyController>().SetLVL(Mathf.Max(1, Mathf.Min(_player._lvl + Random.Range(-1, 2), 10)));
        _enemy.onLVLUp += _leaderboard.UpdateLeaderboard;
        _enemy.onDeath += SpawnEnemy;
        _enemy.onDeath += _leaderboard.UpdateLeaderboard;
    }

    private void Stop()
    {
        _isGameStopped = true;
    }

    public Vector3 GetPlayerPos()
    {
        return _player.transform.position;
    }
}
