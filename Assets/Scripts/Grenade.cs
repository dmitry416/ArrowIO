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
        _rb.AddForce(transform.forward * _speed * _hand._speedBaff * 30);
        Invoke("Explode", _distance * _hand._distanceBaff / 2);
    }
}
