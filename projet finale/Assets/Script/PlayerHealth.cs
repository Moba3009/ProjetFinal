using UnityEngine;
using UnityEngine.SceneManagement;  // Pour g�rer le red�marrage de la sc�ne

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private int _maxHealth = 100;  // Vie maximale du joueur
    [SerializeField] private int _currentHealth;    // Vie actuelle du joueur
    [SerializeField] private GameObject _deathEffect;  // Effet de mort (optionnel)
    [SerializeField] private Animator _animator;     // R�f�rence � l'Animator du joueur
    [SerializeField] private GameObject _defeatUI;   // R�f�rence � l'UI de d�faite (UI qui appara�t lorsque le joueur meurt)

    void Start()
    {
        _currentHealth = _maxHealth;  // Initialise la vie du joueur
    }

    // Cette m�thode est appel�e pour infliger des d�g�ts au joueur
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;  // R�duit la sant� du joueur par la valeur de d�g�ts re�us

        // Si la sant� devient 0 ou moins, le joueur meurt
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    // M�thode pour la mort du joueur
    private void Die()
    {
        // Optionnel : Jouer un effet visuel ou sonore de mort
        if (_deathEffect != null)
        {
            Instantiate(_deathEffect, transform.position, Quaternion.identity);
        }

        // Jouer l'animation de mort (si vous en avez une configur�e)
        if (_animator != null)
        {
            _animator.SetTrigger("Die");
        }

        // Afficher l'UI de d�faite
        if (_defeatUI != null)
        {
            _defeatUI.SetActive(true);  // Activer l'UI de d�faite
        }

        // Optionnel : D�sactiver le joueur apr�s sa mort (ou r�initialiser la sc�ne)
        gameObject.SetActive(false);

        // Mettre le jeu en pause
        Time.timeScale = 0;  // Mettre le jeu en pause lorsque le joueur meurt
    }

    // Optionnel : Afficher la sant� actuelle du joueur (par exemple, dans l'UI)
    public int GetCurrentHealth()
    {
        return _currentHealth;
    }

    // Optionnel : Afficher la sant� maximale du joueur
    public int GetMaxHealth()
    {
        return _maxHealth;
    }

    // M�thode pour red�marrer le niveau
    public void RestartLevel()
    {
        Time.timeScale = 1;  // R�activer le flux du temps
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Recharger la sc�ne actuelle
    }
}
