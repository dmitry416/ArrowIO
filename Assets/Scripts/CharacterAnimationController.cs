using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    private const string isRunning = "isRunning", win = "win", fail = "fail", death = "death", hit = "hit";

    private Animator _animator;

    private void Start()
    {
        UpdateAnimator();
    }

    public void UpdateAnimator()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void Running(bool state)
    {
        _animator.SetBool(isRunning, state);
    }

    public void Win()
    {
        _animator.SetTrigger(win);
    }

    public void Fail()
    {
        _animator.SetTrigger(fail);
    }

    public void Death()
    {
        _animator.SetTrigger(death);
    }

    public void Hit() 
    {
        _animator.SetTrigger(hit);
    }
}
