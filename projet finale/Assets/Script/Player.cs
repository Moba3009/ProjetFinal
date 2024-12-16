using UnityEngine;

public class Player : MonoBehaviour
{
    // Movement
    [Header("Movement Settings")]
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _stoppingDistance = 0.75f;

    // Attack
    [Header("Attack Settings")]
    [SerializeField] private float _attackCooldown = 1.5f;
    [SerializeField] private int _damage = 5;

    // Special Power (AOE)
    [Header("Special Power (AOE) Settings")]
    [SerializeField] private float _aoeRange = 5f;  // Range of the AOE attack
    [SerializeField] private int _aoeDamage = 20;  // Damage dealt by the AOE attack
    [SerializeField] private float _aoeCooldown = 5f;  // Cooldown time for AOE attack
    [SerializeField] private GameObject _aoeEffectPrefab;  // Visual effect for the AOE attack
    private float _nextAoeTime;  // Timestamp for next allowed AOE attack

    // Private variables
    private Animator _animator;
    private Camera _camera;
    private Rigidbody _rigidbody;
    private HealthAndDefense _currentEnemy;
    private Vector3 _targetPosition;
    private bool _attackIsActive;

    // Initialization
    private void Start()
    {
        _camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
    }

    // Update called every frame
    private void Update()
    {
        HandleMovement();
        HandleAttack();
        HandleSpecialPower();
    }

    #region Movement

    // Handles movement and target selection
    private void HandleMovement()
    {
        if (Input.GetMouseButton(0)) // Left mouse button for movement or targeting
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // If the clicked object is an enemy
                HealthAndDefense enemy = hit.collider.GetComponent<HealthAndDefense>();
                if (enemy != null)
                {
                    _currentEnemy = enemy;
                    _attackIsActive = true;
                }
                else // Otherwise, move to the clicked position
                {
                    _currentEnemy = null;
                    _targetPosition = hit.point;
                    LookAtTarget(_targetPosition);
                }
            }
        }

        if (_currentEnemy != null && _currentEnemy.gameObject != null)
        {
            _targetPosition = _currentEnemy.transform.position;
            LookAtTarget(_currentEnemy.transform.position);
        }
        else
        {
            _currentEnemy = null;
            _attackIsActive = false;
        }

        MoveToTarget();
    }

    // Rotates the player to face the target
    private void LookAtTarget(Vector3 target)
    {
        transform.LookAt(target);
    }

    // Moves the player towards the target position
    private void MoveToTarget()
    {
        float distance = Vector3.Distance(transform.position, _targetPosition);
        if (distance > _stoppingDistance)
        {
            Vector3 direction = (_targetPosition - transform.position).normalized;
            _rigidbody.velocity = direction * _movementSpeed;
            _animator.SetBool("IsWalking", true);
        }
        else
        {
            _rigidbody.velocity = Vector3.zero;
            _animator.SetBool("IsWalking", false);
        }
    }

    #endregion

    #region Attacks

    // Handles regular attack logic
    private void HandleAttack()
    {
        if (_attackIsActive && Vector3.Distance(transform.position, _targetPosition) < _stoppingDistance)
        {
            PerformAttack();
        }
    }

    // Executes the attack
    private void PerformAttack()
    {
        if (_currentEnemy == null || _currentEnemy.gameObject == null)
        {
            Debug.LogWarning("Enemy no longer exists. Attack canceled.");
            _attackIsActive = false;
            return;
        }

        _animator.SetBool("IsAttacking", true);
        _attackIsActive = false;
        _currentEnemy.RecieveDamage(_damage);
    }

    // Resets the attack animation (called by Animation Event)
    public void ResetAttackAnimation()
    {
        _animator.SetBool("IsAttacking", false);
    }

    #endregion

    #region Special Power

    // Handles special power (AOE attack) logic
    private void HandleSpecialPower()
    {
        // Ensure AOE attack is triggered only once when the space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= _nextAoeTime)
        {
            UseSpecialPower();
        }
    }

    // Executes the special power
    private void UseSpecialPower()
    {
        Debug.Log("Special Power Activated!");

        // Trigger visual effect
        if (_aoeEffectPrefab != null)
        {
            Instantiate(_aoeEffectPrefab, transform.position, Quaternion.identity);
        }

        // Detect and damage enemies in range
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, _aoeRange);
        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<HealthAndDefense>()?.RecieveDamage(_aoeDamage);
                Debug.Log($"{enemy.name} hit by AOE for {_aoeDamage} damage!");
            }
        }

        // Set cooldown for next AOE attack
        _nextAoeTime = Time.time + _aoeCooldown;
    }

    #endregion

    #region Debug and Gizmos

    // Displays the AOE range in the Unity editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _aoeRange);
    }

    #endregion
}
