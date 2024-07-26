using System.Collections;
using UnityEngine;

public class GrenadeFast : Shuriken
{
    [SerializeField] protected float _radius = 0.5f;
    [SerializeField] protected GameObject _explosionPrefab;

    protected override IEnumerator Fly()
    {
        _startPos = transform.position;
        while (!_collided && Vector3.Distance(transform.position, _startPos) < _distance * _hand._distanceBaff)
        {
            _rb.MovePosition(_rb.position + transform.forward * _speed * _hand._speedBaff * Time.deltaTime * -1);
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
        foreach (Collider hit in Physics.OverlapSphere(transform.position, _radius, 1 << 6))
            if (hit.gameObject.TryGetComponent(out CharacterController enemy))
                if (enemy != _hand.parent)
                    enemy.TakeDamage(_hand.parent, _damage * _hand._damageBaff * (Random.Range(0f, 1f) <= _critChance ? 2 : 1));
        _explosionPrefab.SetActive(true);
        _explosionPrefab.transform.parent = null;
        Destroy(gameObject);
    }
}
