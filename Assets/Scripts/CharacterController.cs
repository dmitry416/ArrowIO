using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CharacterAnimationController))]
public class CharacterController : MonoBehaviour
{
    //[SerializeField] private Weapon _weapon;
    [SerializeField] private Hand _hand;
    [SerializeField] private float _speed;
    [SerializeField] private float _health = 50;
    [SerializeField] private int _lvl = 1;
    [SerializeField] private float _coins = 0;

    private bool isDead = false;
    private Rigidbody _rb;
    private CharacterAnimationController _animController;
    private WeaponPrefabs _weaponPrefabs;

    private void Awake()
    {
        _weaponPrefabs = FindObjectOfType<WeaponPrefabs>();
        _rb = GetComponent<Rigidbody>();
        _animController = GetComponent<CharacterAnimationController>();
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
    }

    public void Rotate(Vector2 direction)
    {
        if (isDead || direction == Vector2.zero)
            return;
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
        if (from == this)
            return;
        _health -= damage;
        if (_health <= 0)
        {
            _health = 0;
            from.AddCoins(_lvl * 10);
            Death();
        }
        else
            _animController.Hit();
    }

    public void AddCoins(int coins)
    {
        _coins += coins;
    }

    private void Death()
    {
        _animController.Death();
        isDead = true;
        Destroy(_hand.gameObject);
        _rb.isKinematic = true;
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject, 5);
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
}
