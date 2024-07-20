using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : Weapon
{
    public override void Go()
    {
        base.Go();
        StartCoroutine(Rotate());
    }

    protected virtual IEnumerator Rotate()
    {
        while (!_collided)
        {
            transform.Rotate(Vector3.forward, 15);
            yield return null;
        }
    }
}
