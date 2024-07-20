using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public CharacterController parent;
    public Weapon weapon;
    public GameObject bow;

    public bool isDouble = false;
    public bool isBack = false;
    public bool isSide = false;
    public bool isFrontSide = false;

    public int _damage;
    public float _critChance;
    public float _distance;
    public float _speed;
    public float _cd;

    private List<Weapon> _weapons = new List<Weapon>();
    private GameObject _bow;
    private float _curTime = 0;

    public void PrepareWeapon()
    {
        _weapons.Clear();

        if (isDouble)
        {

        }
        else
            _weapons.Add(Instantiate(weapon, transform));
        if (isFrontSide)
        {
            
        }
        if (isSide)
        {

        }
        if (isBack)
        {
            
        }

        foreach (Weapon w in _weapons)
            w.transform.parent = transform;
    }

    public void Shoot()
    {
        if (_curTime != 0)
            return;

        foreach (Weapon w in _weapons)
            w.Go();

        _curTime = _cd;
        StartCoroutine(ChangeCD());
    }

    private IEnumerator ChangeCD()
    {
        while (_curTime > 0)
        {
            yield return null;
            _curTime -= Time.deltaTime;
        }
        _curTime = 0;
        PrepareWeapon();
    }

    public void SpawnBow()
    {
        _bow = Instantiate(bow, transform);
    }

    public void DespawnBow()
    {
        if (_bow != null)
            Destroy(_bow);
    }
}
