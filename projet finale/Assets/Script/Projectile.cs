using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;  // Vitesse du projectile
    [SerializeField] private int _damage = 10;  // Dégâts infligés
    [SerializeField] private float _activationDelay = 0.1f;  // Délai avant d'activer les collisions

    private Vector3 _targetDirection;
    private bool _isActive = false;
    private Transform _player;  // Référence au joueur pour suivre sa position

    private void Start()
    {
        // Trouver le joueur au lancement du projectile
        _player = GameObject.FindGameObjectWithTag("Player")?.transform;

        // Assurez-vous que le Collider est bien en mode Trigger
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }
    }

    // Méthode pour définir la direction cible du projectile
    public void SetTarget(Vector3 targetPosition)
    {
        // Initialiser la direction cible vers la position initiale du joueur
        _targetDirection = (targetPosition - transform.position).normalized;
        Invoke(nameof(ActivateCollision), _activationDelay);  // Activer les collisions après un délai
    }

    private void ActivateCollision()
    {
        _isActive = true;  // Permettre les collisions après activation
    }

    void Update()
    {
        // Déplacement du projectile vers la cible uniquement si les collisions sont activées
        if (_isActive)
        {
            // Mise à jour de la direction en fonction de la position actuelle du joueur
            if (_player != null)
            {
                _targetDirection = (_player.position - transform.position).normalized;  // Ré-ajustement de la direction
            }

            // Déplacement du projectile en ligne droite vers la cible
            transform.Translate(_targetDirection * _speed * Time.deltaTime, Space.World);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isActive) return;  // Ignorer les collisions avant activation

        if (other.CompareTag("Player"))
        {
            // Infliger des dégâts au joueur
            other.GetComponent<HealthAndDefense>()?.RecieveDamage(_damage);
            Destroy(gameObject);  // Détruire le projectile après impact
        }
    }
}
