using Cinemachine;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cvc;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private GameUIController _gameUI;
    [Space]
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _playerHero;
    [SerializeField] private int _playerSkin;
    [SerializeField] private int _playerWeapon;

    private HeroPrefabs _heroPrefabs;
    private WeaponPrefabs _weaponPrefabs;
    private CharacterController _player;

    private bool _isGameStopped = false;

    private void Awake()
    {
        _playerHero = YandexGame.savesData.selectedSkin;
        _playerSkin = YandexGame.savesData.selectedStyle;
        _playerWeapon = YandexGame.savesData.selectedWeapon;

        _heroPrefabs = FindObjectOfType<HeroPrefabs>();
        _weaponPrefabs = FindObjectOfType<WeaponPrefabs>();
        _player = Instantiate(_playerPrefab).GetComponent<CharacterController>();
        _player.SetHero(_heroPrefabs.GetHero(_playerHero));
        _player.SetSkin(_playerSkin);
        _player.SetWeapon(_playerWeapon);
        _cvc.Follow = _player.gameObject.transform;

        if (SceneManager.GetActiveScene().buildIndex == 1)
            _player.onDeath += () => _gameUI.EndPanel("ÎÒËÈ×ÍÎ");
        else
        {
            _gameUI.SetTimer();
            _gameUI.onTimerEnd += () => _gameUI.EndPanel("ÂÐÅÌß ÊÎÍ×ÈËÎÑÜ");
            _gameUI.onTimerEnd += StopCharacters;
        }

        for (int i = 0; i < _enemySpawner._enemyCount; ++i)
            SpawnEnemy();
    }

    private void StopCharacters()
    {
        _isGameStopped = true;
        foreach (CharacterController character in FindObjectsOfType<CharacterController>())
            character.Stop();
    }
    public void SpawnEnemy()
    {
        if (_isGameStopped)
            return;
        Vector3 position = _enemySpawner.GetPos();
        CharacterController _enemy = Instantiate(_enemyPrefab, position, _enemyPrefab.transform.rotation).GetComponent<CharacterController>();
        _enemy.SetHero(_heroPrefabs.GetHero(Random.Range(0, _heroPrefabs.GetHeroesLength())));
        _enemy.SetSkin(Random.Range(0, 5));
        _enemy.SetWeapon(Random.Range(0, _weaponPrefabs.GetWeaponsLength()));
        _enemy.GetComponent<EnemyController>().SetLVL(Mathf.Min(1, _player._lvl + Random.Range(-1, 2)));
        _enemy.onDeath += SpawnEnemy;
    }

    public Vector3 GetPlayerPos()
    {
        return _player.transform.position;
    }
}
