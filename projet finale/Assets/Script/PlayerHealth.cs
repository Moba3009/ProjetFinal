using UnityEngine;
using UnityEngine.SceneManagement;  // Pour gérer le redémarrage de la scène

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private int _maxHealth = 100;  // Vie maximale du joueur
    [SerializeField] private int _currentHealth;    // Vie actuelle du joueur
    [SerializeField] private GameObject _deathEffect;  // Effet de mort (optionnel)
    [SerializeField] private Animator _animator;     // Référence à l'Animator du joueur
    [SerializeField] private GameObject _defeatUI;   // Référence à l'UI de défaite (UI qui apparaît lorsque le joueur meurt)

    void Start()
    {
        _currentHealth = _maxHealth;  // Initialise la vie du joueur
    }

    // Cette méthode est appelée pour infliger des dégâts au joueur
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;  // Réduit la santé du joueur par la valeur de dégâts reçus

        // Si la santé devient 0 ou moins, le joueur meurt
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    // Méthode pour la mort du joueur
    private void Die()
    {
        // Optionnel : Jouer un effet visuel ou sonore de mort
        if (_deathEffect != null)
        {
            Instantiate(_deathEffect, transform.position, Quaternion.identity);
        }

        // Jouer l'animation de mort (si vous en avez une configurée)
        if (_animator != null)
        {
            _animator.SetTrigger("Die");
        }

        // Afficher l'UI de défaite
        if (_defeatUI != null)
        {
            _defeatUI.SetActive(true);  // Activer l'UI de défaite
        }

        // Optionnel : Désactiver le joueur après sa mort (ou réinitialiser la scène)
        gameObject.SetActive(false);

        // Mettre le jeu en pause
        Time.timeScale = 0;  // Mettre le jeu en pause lorsque le joueur meurt
    }

    // Optionnel : Afficher la santé actuelle du joueur (par exemple, dans l'UI)
    public int GetCurrentHealth()
    {
        return _currentHealth;
    }

    // Optionnel : Afficher la santé maximale du joueur
    public int GetMaxHealth()
    {
        return _maxHealth;
    }

    // Méthode pour redémarrer le niveau
    public void RestartLevel()
    {
        Time.timeScale = 1;  // Réactiver le flux du temps
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Recharger la scène actuelle
    }
}
