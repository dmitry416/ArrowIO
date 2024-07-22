using UnityEngine;

public class DiamondSpawner : MonoBehaviour
{
    [SerializeField] private Transform _leftDownCorner;
    [SerializeField] private Transform _rightUpCorner;
    [SerializeField] private GameObject _diamondPref;
    [SerializeField] private int _diamondCount = 10;

    private void Awake()
    {
        for (int i = 0; i < _diamondCount; ++i)
        {
            Diamond diamond = Instantiate(_diamondPref).GetComponent<Diamond>();
            diamond._leftDownCorner = _leftDownCorner;
            diamond._rightUpCorner = _rightUpCorner;
            diamond.Locate();
        }
    }
}
