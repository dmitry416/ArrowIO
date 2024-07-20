using System.Collections;
using UnityEngine;

public class Boomerang : Shuriken
{
    protected override IEnumerator Fly()
    {
        _startPos = transform.position;
        while (!_collided && Vector3.Distance(transform.position, _startPos) < _hand._distance)
        {
            _rb.MovePosition(_rb.position + transform.forward * _hand._speed * Time.deltaTime * -1);
            yield return null;
        }
        if (!_collided)
            StartCoroutine(FlyBack());
    }

    protected IEnumerator FlyBack()
    {
        while (!_collided && Vector3.Distance(transform.position, _startPos) > 0.1f)
        {
            _rb.MovePosition(_rb.position + (_startPos - transform.position).normalized * _hand._speed * Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (!_started)
            return;
        if (other.TryGetComponent(out CharacterController enemy))
        {
            if (enemy == _hand.parent)
                return;
            enemy.TakeDamage(_hand.parent, _hand._damage);
            Destroy(gameObject);
        }
        Destroy(gameObject, 3);
        GetComponent<Collider>().enabled = false;
        _collided = true;
    }
}
