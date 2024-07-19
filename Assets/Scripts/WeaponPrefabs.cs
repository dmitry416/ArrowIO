using UnityEngine;

public class WeaponPrefabs : ObjectPrefabs
{
    public GameObject GetWeapon(int index)
    {
        return GetObject(index);
    }

    public int GetWeaponsLength()
    {
        return GetObjectsLength();
    }
}
