using UnityEngine;

public class Heal : MonoBehaviour
{
    public Transform _leftDownCorner;
    public Transform _rightUpCorner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterController character))
        {
            character.AddHealth(25);
            Locate();
        }
    }

    public void Locate()
    {
        transform.position = new Vector3(
            Random.Range(_leftDownCorner.transform.position.x, _rightUpCorner.transform.position.x),
            0.1f,
            Random.Range(_leftDownCorner.transform.position.z, _rightUpCorner.transform.position.z));
        if (Physics.CheckSphere(transform.position + Vector3.up, 1))
            Locate();
    }
}
