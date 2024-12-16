using UnityEngine;

public class HealthAndDefense : MonoBehaviour
{
    [SerializeField] private int _health = 100;  // Points de vie actuels
    [SerializeField] private GameObject deathEffectPrefab;  // Effet visuel lors de la mort (facultatif)

    // Méthode pour recevoir des dégâts
    public void RecieveDamage(int damage)
    {
        _health -= damage;
        Debug.Log($"{gameObject.name} a subi {damage} points de dégâts. Santé restante : {_health}");

        if (_health <= 0)
        {
            Die();  // Appeler la méthode de mort si la santé est à 0
        }
    }

    // Méthode appelée lorsque l'entité meurt
    private void Die()
    {
        Debug.Log($"{gameObject.name} est mort !");

        // Facultatif : Instancier un effet visuel lors de la mort
        if (deathEffectPrefab != null)
        {
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        }

        // Détruire l'objet
        Destroy(gameObject);
    }
}
