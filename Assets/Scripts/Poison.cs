using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Poison : GrenadeFast
{
    [SerializeField] protected float _delta = 1f;
    protected List<CharacterControllerMy> _poisonedEnemys = new List<CharacterControllerMy>();
    protected override void Explode()
    {
        foreach (Collider hit in Physics.OverlapSphere(transform.position, _radius, 1 << 6))
            if (hit.gameObject.TryGetComponent(out CharacterControllerMy enemy))
                if (enemy != _hand.parent)
                    _poisonedEnemys.Add(enemy);
        Invoke("PoisonEnemys", _delta);
        Invoke("PoisonEnemys", _delta * 2);
        Invoke("PoisonEnemys", _delta * 3);
        _explosionPrefab.SetActive(true);
        _explosionPrefab.transform.parent = null;
        gameObject.SetActive(false);
        Destroy(gameObject, _delta * 3 + 1);
    }

    protected void PoisonEnemys()
    {
        foreach (CharacterControllerMy enemy in _poisonedEnemys)
            enemy.TakeDamage(_hand.parent, _damage * _hand._damageBaff * (Random.Range(0, 1) <= _critChance ? 2 : 1));
    }
}
