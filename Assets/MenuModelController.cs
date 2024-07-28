using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuModelController : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _animator.SetTrigger("win");
    }
}
