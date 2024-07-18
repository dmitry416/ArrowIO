using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CharacterAnimationController))]
public class CharacterController : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Rigidbody _rb;
    private CharacterAnimationController _animController;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animController = GetComponent<CharacterAnimationController>();
    }

    public void Move(Vector2 direction)
    {
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
        if (direction == Vector2.zero)
            return;
        transform.rotation = Quaternion.Euler(0, Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg, 0);
    }

    public void Shoot() 
    {

    }
}
