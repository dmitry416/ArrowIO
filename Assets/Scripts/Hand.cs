﻿using System.Collections;
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

    public int _damageBaff = 1;
    public float _critChanceBaff = 1;
    public float _distanceBaff = 1;
    public float _speedBaff = 1;
    public float _cd = 1;

    private List<Weapon> _weapons = new List<Weapon>();
    private GameObject _bow;
    private float _curTime = 0;

    public void PrepareWeapon()
    {
        _weapons.Clear();

        if (isDouble)
        {
            _weapons.Add(Instantiate(weapon, transform));
            _weapons[_weapons.Count - 1].transform.localPosition += new Vector3(0.2f, 0, 0);
            _weapons.Add(Instantiate(weapon, transform));
            _weapons[_weapons.Count - 1].transform.localPosition -= new Vector3(0.2f, 0, 0);
        }
        else
            _weapons.Add(Instantiate(weapon, transform));
        if (isFrontSide)
        {
            _weapons.Add(Instantiate(weapon, transform));
            _weapons[_weapons.Count - 1].transform.Rotate(new Vector3(0, 45, 0));
            _weapons.Add(Instantiate(weapon, transform));
            _weapons[_weapons.Count - 1].transform.Rotate(new Vector3(0, -45, 0));
        }
        if (isSide)
        {
            _weapons.Add(Instantiate(weapon, transform));
            _weapons[_weapons.Count - 1].transform.Rotate(new Vector3(0, 90, 0));
            _weapons.Add(Instantiate(weapon, transform));
            _weapons[_weapons.Count - 1].transform.Rotate(new Vector3(0, -90, 0));
        }
        if (isBack)
        {
            _weapons.Add(Instantiate(weapon, transform));
            _weapons[_weapons.Count - 1].transform.Rotate(new Vector3(180, 0, 0));
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