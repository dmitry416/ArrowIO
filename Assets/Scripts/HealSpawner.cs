using UnityEngine;

public class HealSpawner : MonoBehaviour
{
    [SerializeField] private Transform _leftDownCorner;
    [SerializeField] private Transform _rightUpCorner;
    [SerializeField] private GameObject _healPref;
    [SerializeField] private int _healCount = 3;

    private void Awake()
    {
        for (int i = 0; i < _healCount; ++i)
        {
            Heal heal = Instantiate(_healPref).GetComponent<Heal>();
            heal._leftDownCorner = _leftDownCorner;
            heal._rightUpCorner = _rightUpCorner;
            heal.Locate();
        }
    }
}
