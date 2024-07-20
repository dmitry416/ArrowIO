using System.Collections;
using UnityEngine;

public class GrenadeFast : Shuriken
{
    [SerializeField] protected float _radius = 0.5f;
    [SerializeField] protected GameObject _explosionPrefab;

    protected override IEnumerator Fly()
    {
        _startPos = transform.position;
        while (!_collided && Vector3.Distance(transform.position, _startPos) < _hand._distance)
        {
            _rb.MovePosition(_rb.position + transform.forward * _hand._speed * Time.deltaTime * -1);
            yield return null;
        }
        Explode();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (!_started)
            return;
        if (other.TryGetComponent(out CharacterController me))
            if (me == _hand.parent)
                return;
        Explode();
    }

    protected virtual void Explode()
    {
        foreach (Collider hit in Physics.OverlapSphere(transform.position, _radius))
            if (hit.gameObject.TryGetComponent(out CharacterController enemy))
                if (enemy != _hand.parent)
                    enemy.TakeDamage(_hand.parent, _hand._damage);
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
