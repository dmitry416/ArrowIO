using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _leftDownCorner;
    [SerializeField] private Transform _rightUpCorner;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] public int _enemyCount = 3;

    public Vector3 GetPos()
    {
        Vector3 pos = new Vector3(
                Random.Range(_leftDownCorner.transform.position.x, _rightUpCorner.transform.position.x),
                0.1f,
                Random.Range(_leftDownCorner.transform.position.z, _rightUpCorner.transform.position.z));
        while (Vector3.Distance(pos, _gameManager.GetPlayerPos()) < 5 || Physics.CheckSphere(pos + Vector3.up * 0.5f, 0.5f))
        {
            pos = new Vector3(
                Random.Range(_leftDownCorner.transform.position.x, _rightUpCorner.transform.position.x),
                0.1f,
                Random.Range(_leftDownCorner.transform.position.z, _rightUpCorner.transform.position.z));
        }
        return pos;
    }
}
