using DG.Tweening;
using System;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    public Transform _leftDownCorner;
    public Transform _rightUpCorner;

    private Tween[] _animTweens = new Tween[0];

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterController character))
        {
            character.AddCoins(1);
            Locate();
        }
    }

    private void Start()
    {
        Animate();
    }

    public void Locate()
    {
        foreach (Tween t in _animTweens)
            t.Kill();
        transform.position = new Vector3(
            UnityEngine.Random.Range(_leftDownCorner.transform.position.x, _rightUpCorner.transform.position.x),
            0.1f,
            UnityEngine.Random.Range(_leftDownCorner.transform.position.z, _rightUpCorner.transform.position.z));
        Animate();
        if (Physics.CheckSphere(transform.position + Vector3.up, 1))
            Locate();
    }
    
    private void Animate()
    {
        foreach (Tween t in _animTweens)
            t.Kill();
        _animTweens = new Tween[2];
        _animTweens[0] = transform.DORotate(new Vector3(-90, 180, 0), 1).SetLoops(-1).SetEase(Ease.Linear);
        _animTweens[1] = transform.DOPunchPosition(Vector3.up * 0.3f, 1, 1, 1).SetLoops(-1);
    }

    private void OnDisable()
    {
        foreach (Tween t in _animTweens)
            t.Kill();
    }
}
