using UnityEngine;
using UnityEngine.AI;

public class RangedEnemy : MonoBehaviour
{
    [SerializeField] private float _shootRange = 10f; 
    [SerializeField] private GameObject _projectilePrefab; 
    [SerializeField] private Transform _firePoint; 
    [SerializeField] private float _fireRate = 1f; 

    private Transform _player;  
    private NavMeshAgent _agent;  
    private float _nextFireTime;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
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

        // Si l'ennemi est à portée de tir
        if (distanceToPlayer <= _shootRange)
        {
            _agent.isStopped = true;  // L'ennemi s'arrête pour tirer

            // Si c'est le moment de tirer, procéder
            if (Time.time >= _nextFireTime)
            {
                Shoot();  // Lancer un projectile
                _nextFireTime = Time.time + 1f / _fireRate;  // Réinitialiser le cooldown entre les tirs
            }
        }
        else
        {
            _agent.isStopped = false;  // L'ennemi se déplace vers le joueur si hors de portée
            _agent.SetDestination(_player.position);
        }
    }

    private void Shoot()
    {
        if (_player == null) return;

        // Instancier le projectile depuis le firePoint
        GameObject projectileInstance = Instantiate(_projectilePrefab, _firePoint.position, Quaternion.identity);

        // Passer la position du joueur au projectile
        Projectile projectileScript = projectileInstance.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.SetTarget(_player.position);  // Définir la cible pour le projectile
        }

        // Facultatif : Faire en sorte que l'ennemi regarde le joueur lorsqu'il tire
        transform.LookAt(_player.position);
    }
}
