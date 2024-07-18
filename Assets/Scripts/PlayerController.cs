using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour 
{
    private CharacterController _charCont;
    private PlayerInput _input;

    private void Awake()
    {
        _charCont = GetComponent<CharacterController>();

        _input = new PlayerInput();
        _input.Player.Shoot.performed += context => _charCont.Shoot();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Update()
    {
        Vector2 dir = _input.Player.Move.ReadValue<Vector2>();
        _charCont.Move(dir);
        _charCont.Rotate(dir);
    }
}
