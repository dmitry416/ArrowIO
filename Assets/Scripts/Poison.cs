using System.Collections.Generic;
using UnityEngine;

public class Poison : GrenadeFast
{
    [SerializeField] protected float _delta = 1f;
    protected List<CharacterController> _poisonedEnemys = new List<CharacterController>();
    protected override void Explode()
    {
        foreach (Collider hit in Physics.OverlapSphere(transform.position, _radius))
            if (hit.gameObject.TryGetComponent(out CharacterController enemy))
                if (enemy != _hand.parent)
                    _poisonedEnemys.Add(enemy);
        Invoke("PoisonEnemys", _delta);
        Invoke("PoisonEnemys", _delta * 2);
        Invoke("PoisonEnemys", _delta * 3);
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    protected void PoisonEnemys()
    {
        foreach (CharacterController enemy in _poisonedEnemys)
            enemy.TakeDamage(_hand.parent, _damage * _hand._damageBaff * (Random.Range(0, 1) <= _critChance ? 2 : 1));
    }
}
