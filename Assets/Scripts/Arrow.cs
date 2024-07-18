using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Arrow : MonoBehaviour
{
    private Bow _parent;
    private Vector3 _startPos;
    private bool _collided = false;
    private bool _started = false;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Go()
    {
        _rb.isKinematic = false;
        _parent = transform.parent.GetComponent<Bow>();
        transform.parent = null;
        _started = true;
        StartCoroutine(Fly());
    }

    private IEnumerator Fly()
    {
        _startPos = transform.position;
        while (!_collided && Vector3.Distance(transform.position, _startPos) < _parent._distance)
        {
            _rb.MovePosition(_rb.position + transform.forward * _parent._speed * Time.deltaTime * -1);
            yield return null;
        }
        if (!_collided)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_started)
            return;
        if (other.TryGetComponent(out CharacterController enemy))
        {
            if (enemy == _parent.parent)
                return;
            enemy.TakeDamage(_parent.parent, _parent._damage);
            Destroy(gameObject);
        }
        Destroy(gameObject, 3);
        _collided = true;
    }
}
