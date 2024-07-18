using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    [SerializeField] private Arrow _arrow;

    private List<Arrow> _curArrows = new List<Arrow>();

    private void Start()
    {
        Reload();
    }

    public override void Shoot()
    {
        if (!CanShoot())
            return;
        base.Shoot();

        foreach (Arrow arrow in _curArrows)
            arrow.Go();
    }

    protected override void Reload()
    {
        _curArrows.Clear();
        _curArrows.Add(Instantiate(_arrow, transform.position, transform.rotation));
        foreach (Arrow arrow in _curArrows)
        {
            arrow.transform.Rotate(new Vector3(0, 90, 0));
            arrow.transform.parent = transform;
        }
    }
}
