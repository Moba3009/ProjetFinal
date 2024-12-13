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
        if (Input.GetMouseButton(0))  // Clic gauche de la souris pour s�lectionner un ennemi ou une position
        {
            Ray ray;
            RaycastHit hit;
            ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                HealthAndDefense enemy = hit.collider.GetComponent<HealthAndDefense>();

                if (enemy != null) // Si un ennemi est touch�
                {
                    _currentEnemy = enemy;
                    _attackIsActive = true; // L'attaque est pr�te
                }
                else // Si ce n'est pas un ennemi, d�placer vers la position cible
                {
                    _currentEnemy = null;
                    _targetPosition = hit.point;
                    transform.LookAt(_targetPosition);
                }
            }
        }

        if (_currentEnemy != null)
        {
            // Si l'ennemi est toujours pr�sent, mettre � jour la position cible pour le suivre
            if (_currentEnemy.gameObject != null)
            {
                _targetPosition = _currentEnemy.transform.position;
                transform.LookAt(_currentEnemy.transform.position); // Regarder vers l'ennemi
            }
            else
            {
                // Si l'ennemi a �t� d�truit
                _currentEnemy = null;
                _attackIsActive = false;
            }
        }

        float distance = (transform.position - _targetPosition).magnitude;
        Vector3 direction = (_targetPosition - transform.position).normalized;

        if (distance > _stopingdistance)  // Si la distance est plus grande que la distance d'arr�t
        {
            _rigidbody.velocity = _movementSpeed * direction;
            _animator.SetBool("IsWalking", true);
        }
        else  // Si le joueur est assez pr�s
        {
            _animator.SetBool("IsWalking", false);
            _rigidbody.velocity = Vector3.zero;  // Arr�ter le joueur
        }

        // Si l'attaque est pr�te et le joueur est � port�e de l'ennemi
        if (_attackIsActive && distance < _stopingdistance)
        {
            Attack();
        }
    }

    private void Attack()
    {
        // Si l'ennemi est null, �viter d'attaquer
        if (_currentEnemy == null || _currentEnemy.gameObject == null)
        {
            Debug.LogWarning("Enemy no longer exists. Attack canceled.");
            _attackIsActive = false;
            return;
        }

        _animator.SetBool("IsAttacking", true);  // Jouer l'animation d'attaque
        _attackIsActive = false;  // D�sactiver l'attaque jusqu'� la prochaine interaction
        _currentEnemy.RecieveDamage(_damage);  // Infliger des d�g�ts � l'ennemi
    }

    // Fonction pour r�initialiser l'animation d'attaque apr�s qu'elle soit termin�e
    public void ResetAttack()
    {
        _animator.SetBool("IsAttacking", false);
    }
}
