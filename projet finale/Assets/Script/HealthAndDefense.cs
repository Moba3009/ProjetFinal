using UnityEngine;

public class HealthAndDefense : MonoBehaviour
{
    [SerializeField] private int _health = 100;  // Points de vie actuels
    [SerializeField] private GameObject deathEffectPrefab;  // Effet visuel lors de la mort (facultatif)

    // M�thode pour recevoir des d�g�ts
    public void RecieveDamage(int damage)
    {
        _health -= damage;
        Debug.Log($"{gameObject.name} a subi {damage} points de d�g�ts. Sant� restante : {_health}");

        if (_health <= 0)
        {
            Die();  // Appeler la m�thode de mort si la sant� est � 0
        }
    }

    // M�thode appel�e lorsque l'entit� meurt
    private void Die()
    {
        Debug.Log($"{gameObject.name} est mort !");

        // Facultatif : Instancier un effet visuel lors de la mort
        if (deathEffectPrefab != null)
        {
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        }

        // D�truire l'objet
        Destroy(gameObject);
    }
}
