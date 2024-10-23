using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Weapon : MonoBehaviour
{
    protected Hand _hand;
    protected Vector3 _startPos;
    protected bool _collided = false;
    protected bool _started = false;
    protected Rigidbody _rb;

    public int _damage;
    public float _critChance;
    public float _distance;
    public float _speed;

    protected void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _hand = GetComponentInParent<Hand>();
    }

    protected virtual IEnumerator Fly()
    {
        _startPos = transform.position;
        while (!_collided && Vector3.Distance(transform.position, _startPos) < _distance * _hand._distanceBaff)
        {
            _rb.MovePosition(_rb.position + transform.forward * _speed * _hand._speedBaff * Time.deltaTime * -1);
            yield return null;
        }
        if (!_collided)
            Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!_started)
            return;
        if (other.TryGetComponent(out CharacterControllerMy enemy))
        {
            if (enemy == _hand.parent)
                return;
            enemy.TakeDamage(_hand.parent, _damage * _hand._damageBaff * (Random.Range(0, 1) <= _critChance + _hand._critChanceBaff ? 2 : 1));
            Destroy(gameObject);
        }
        Destroy(gameObject, 3);
        GetComponent<Collider>().enabled = false;
        _collided = true;
    }

    public virtual void Go()
    {
        _rb.isKinematic = false;
        transform.parent = null;
        _started = true;
        StartCoroutine(Fly());
    }
}
