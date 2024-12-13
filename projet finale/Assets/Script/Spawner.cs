using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _minSpeed = 2f;
    [SerializeField] private float _maxSpeed = 10f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnObject();
        }
    }

    private void SpawnObject()
    {

        GameObject spawnedObject = Instantiate(_prefab, transform.position, Quaternion.identity);


        Vector3 randomDirection = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)).normalized;


        float randomSpeed = Random.Range(_minSpeed, _maxSpeed);

        Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = randomDirection * randomSpeed;
        }

        GameManager._instance.IncreaseNumberObject();
    }
}
