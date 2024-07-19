using UnityEngine;

public class ObjectPrefabs : MonoBehaviour
{
    [SerializeField] protected GameObject[] _objects;

    protected GameObject GetObject(int index)
    {
        return _objects[index];
    }

    protected int GetObjectsLength()
    {
        return _objects.Length;
    }
}
