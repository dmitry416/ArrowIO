using System.Collections;
using UnityEngine;

public class Grenade : GrenadeFast
{
    protected override void OnTriggerEnter(Collider other)
    {
        return;
    }

    public override void Go()
    {
        _rb.isKinematic = false;
        transform.parent = null;
        _started = true;
        _rb.AddForce(transform.forward * _hand._speed * 30);
        Invoke("Explode", _hand._distance / 2);
    }
}
