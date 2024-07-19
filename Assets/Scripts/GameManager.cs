using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cvc;
    [Space]
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private int _playerHero;
    [SerializeField] private int _playerSkin;
    [SerializeField] private int _playerWeapon;

    private HeroPrefabs _heroPrefabs;
    private CharacterController _player;

    private void Start()
    {
        _heroPrefabs = FindObjectOfType<HeroPrefabs>();
        _player = Instantiate(_playerPrefab).GetComponent<CharacterController>();
        _player.SetHero(_heroPrefabs.GetHero(_playerHero));
        _player.SetSkin(_playerSkin);
        _player.SetWeapon(_playerWeapon);
        _cvc.Follow = _player.gameObject.transform;
    }
}
