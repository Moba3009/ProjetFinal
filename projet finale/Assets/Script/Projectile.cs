using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private int _damage = 10;

    private Transform _target;

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    void Update()
    {
        if (_target != null)
        {
            MoveTowardsTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        transform.position += direction * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Applique les dégâts au joueur
            other.GetComponent<PlayerHealth>()?.TakeDamage(_damage);  // Appelle la méthode TakeDamage dans PlayerHealth
            Destroy(gameObject);  // Détruit le projectile après l'impact
        }
    }
}
