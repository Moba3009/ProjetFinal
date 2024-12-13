using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _stopingdistance = 0.75f;
    [SerializeField] private float _attackCoolDown = 1.5f;
    [SerializeField] private int _damage = 5;

    private Animator _animator;
    private Camera _camera;
    private Rigidbody _rigidbody;
    private HealthAndDefense _currentEnemy;
    private Vector3 _targetPosition;
    private bool _attackIsActive;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))  // Clic gauche de la souris pour sélectionner un ennemi ou une position
        {
            Ray ray;
            RaycastHit hit;
            ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                HealthAndDefense enemy = hit.collider.GetComponent<HealthAndDefense>();

                if (enemy != null) // Si un ennemi est touché
                {
                    _currentEnemy = enemy;
                    _attackIsActive = true; // L'attaque est prête
                }
                else // Si ce n'est pas un ennemi, déplacer vers la position cible
                {
                    _currentEnemy = null;
                    _targetPosition = hit.point;
                    transform.LookAt(_targetPosition);
                }
            }
        }

        if (_currentEnemy != null)
        {
            // Si l'ennemi est toujours présent, mettre à jour la position cible pour le suivre
            if (_currentEnemy.gameObject != null)
            {
                _targetPosition = _currentEnemy.transform.position;
                transform.LookAt(_currentEnemy.transform.position); // Regarder vers l'ennemi
            }
            else
            {
                // Si l'ennemi a été détruit
                _currentEnemy = null;
                _attackIsActive = false;
            }
        }

        float distance = (transform.position - _targetPosition).magnitude;
        Vector3 direction = (_targetPosition - transform.position).normalized;

        if (distance > _stopingdistance)  // Si la distance est plus grande que la distance d'arrêt
        {
            _rigidbody.velocity = _movementSpeed * direction;
            _animator.SetBool("IsWalking", true);
        }
        else  // Si le joueur est assez près
        {
            _animator.SetBool("IsWalking", false);
            _rigidbody.velocity = Vector3.zero;  // Arrêter le joueur
        }

        // Si l'attaque est prête et le joueur est à portée de l'ennemi
        if (_attackIsActive && distance < _stopingdistance)
        {
            Attack();
        }
    }

    private void Attack()
    {
        // Si l'ennemi est null, éviter d'attaquer
        if (_currentEnemy == null || _currentEnemy.gameObject == null)
        {
            Debug.LogWarning("Enemy no longer exists. Attack canceled.");
            _attackIsActive = false;
            return;
        }

        _animator.SetBool("IsAttacking", true);  // Jouer l'animation d'attaque
        _attackIsActive = false;  // Désactiver l'attaque jusqu'à la prochaine interaction
        _currentEnemy.RecieveDamage(_damage);  // Infliger des dégâts à l'ennemi
    }

    // Fonction pour réinitialiser l'animation d'attaque après qu'elle soit terminée
    public void ResetAttack()
    {
        _animator.SetBool("IsAttacking", false);
    }
}
