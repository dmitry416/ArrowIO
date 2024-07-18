using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterController : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector2 direction)
    {
        _rb.MovePosition(_rb.position + new Vector3(direction.x, 0, direction.y) * Time.deltaTime * _speed);
    }

    public void Shoot()
    {

    }
}
