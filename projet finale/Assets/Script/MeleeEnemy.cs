using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private float _movementSpeed = 1f;  // Vitesse de déplacement
    [SerializeField] private float _attackRange = 1.5f;  // Portée d'attaque
    [SerializeField] private float _attackCooldown = 2f; // Temps entre deux attaques
    [SerializeField] private int _damage = 10; // Dégâts infligés au joueur
    [SerializeField] private int _maxHealth = 100; // Vie maximale

    private Transform _player; // Référence au joueur
    private AnimationLinker _animationLinker; // Référence pour l'animation
    private float _lastAttackTime; // Temps du dernier attaque
    private int _currentHealth; // Vie actuelle
    private bool _isAttacking; // Vérifie si l'ennemi est en train d'attaquer

    // Start is called before the first frame update
    void Start()
    {
        _animationLinker = GetComponentInChildren<AnimationLinker>();  // Récupère l'animation
        _currentHealth = _maxHealth;  // Initialise la vie de l'ennemi
        _player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (_player == null || _currentHealth <= 0) return;  // Si pas de joueur ou ennemi mort, on arrête le processus

        float distanceToHero = Vector3.Distance(transform.position, _player.position); // Calcul de la distance

        if (distanceToHero > _attackRange)  // Si le joueur est hors de portée
        {
            FollowHero();  // L'ennemi suit le joueur
        }
        else  // Si l'ennemi est à portée
        {
            if (!_isAttacking && Time.time > _lastAttackTime + _attackCooldown)  // Si l'ennemi peut attaquer
            {
                StartCoroutine(AttackHero());  // Attaque l'ennemi
            }
        }
    }

    // Gère le suivi du joueur
    private void FollowHero()
    {
        Vector3 direction = (_player.position - transform.position).normalized;  // Direction vers le joueur
        transform.LookAt(_player.position);  // Tourne l'ennemi pour faire face au joueur

        transform.position += direction * _movementSpeed * Time.deltaTime;  // Déplace l'ennemi vers le joueur
        _animationLinker.Walk();  // Active l'animation de marche
    }

    // Coroutine pour effectuer l'attaque du joueur
    private System.Collections.IEnumerator AttackHero()
    {
        _isAttacking = true;  // L'ennemi commence l'attaque
        _animationLinker.Attack();  // Active l'animation d'attaque

        yield return new WaitForSeconds(0.5f);  // Attends que l'animation se termine (ajustez la durée en fonction de l'animation)

        /*// Si l'ennemi est toujours à portée
        if (Vector3.Distance(transform.position, _hero.position) <= _attackRange)
        {
            //HeroHealth._Instance.TakeDamage(_damage);  // Inflige des dégâts au joueur
        }*/

        _lastAttackTime = Time.time;  // Met à jour le temps du dernier attaque
        _isAttacking = false;  // L'ennemi n'attaque plus
    }

    
    private void Die()
    {
        _animationLinker.Death(); 
        Destroy(gameObject, 2f);  
    }

    // Méthode pour recevoir des dégâts
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;  
        if (_currentHealth <= 0)  
        {
            Die();
        }
    }
}
