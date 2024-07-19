using UnityEngine;

public class HeroPrefabs : ObjectPrefabs
{
    public GameObject GetHero(int index)
    {
        return GetObject(index);
    }

    public int GetHeroesLength()
    {
        return GetObjectsLength();
    }
}
