using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float _attackRange = 1.5f;
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _attackCooldown = 1f;

    private Transform _player;
    private NavMeshAgent _agent;
    private float _nextAttackTime;

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
        if (_player == null || !_agent.isOnNavMesh) return; // Vérification

        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

        if (distanceToPlayer <= _attackRange)
        {
            _agent.isStopped = true;

            if (Time.time >= _nextAttackTime)
            {
                Attack();
                _nextAttackTime = Time.time + _attackCooldown;
            }
        }
        else
        {
            _agent.isStopped = false;
            _agent.SetDestination(_player.position);
        }
    }

    private void Attack()
    {
        Debug.Log($"{gameObject.name} attacks the player!");
        _player.GetComponent<HealthAndDefense>()?.RecieveDamage(_damage);
    }
}
