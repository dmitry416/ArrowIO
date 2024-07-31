using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    private const string isRunning = "isRunning", win = "win", fail = "fail", death = "death", hit = "hit";

    private Animator _animator;
    private bool _isDead = false;

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
        if (_isDead)
            return;
        _animator.SetBool(isRunning, state);
    }

    public void Win()
    {
        if (_isDead)
            return;
        _animator.SetTrigger(win);
    }

    public void Fail()
    {
        _animator.SetTrigger(fail);
    }

    public void Death()
    {
        _isDead = true;
        _animator.SetTrigger(death);
    }

    public void Respawn()
    {
        _isDead = false;
        Hit();
    }

    public void Hit() 
    {
        if (_isDead)
            return;
        _animator.SetTrigger(hit);
    }
}
