using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform _camera;

    private void Start()
    {
        _camera = Camera.main.transform;
    }
    private void Update()
    {
        transform.rotation = _camera.transform.rotation;
    }
}
