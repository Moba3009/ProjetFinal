using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [SerializeField] private float _shootRange = 10f;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireRate = 1f;

    private Transform _player;
    private float _nextFireTime;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (_player == null)
        {
            Debug.LogError("Player not found! Ensure the player has the 'Player' tag.");
        }
    }

    private void Update()
    {
        if (_player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

        if (distanceToPlayer <= _shootRange)
        {
            if (Time.time >= _nextFireTime)
            {
                Shoot();
                _nextFireTime = Time.time + 1f / _fireRate;
            }
        }
        else
        {
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (_player.position - transform.position).normalized;
        transform.LookAt(new Vector3(_player.position.x, transform.position.y, _player.position.z));
        transform.position += direction * Time.deltaTime;
    }

    private void Shoot()
    {
        if (_player == null) return;

        GameObject projectileInstance = Instantiate(_projectilePrefab, _firePoint.position, Quaternion.identity);
        Projectile projectileScript = projectileInstance.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.SetTarget(_player.transform);
        }
    }
}