using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public CharacterController parent;
    public int _damage;
    public float _critChance;
    public float _distance;
    public float _speed;
    public float _cd;

    protected float _curTime = 0;

    public virtual void Shoot()
    {
        _curTime = _cd;
        StartCoroutine(ChangeCD());
    }

    protected bool CanShoot()
    {
        return _curTime == 0;
    }

    protected virtual void Reload() { }

    private IEnumerator ChangeCD()
    {
        while (_curTime > 0) 
        { 
            yield return null;
            _curTime -= Time.deltaTime;
        }
        _curTime = 0;
        Reload();
    }
}
