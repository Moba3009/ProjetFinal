using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;  // Référence à la barre de vie (Slider)
    [SerializeField] private PlayerHealth _playerHealth;  // Référence au script PlayerHealth

    void Start()
    {
        // Initialise la barre de vie avec la valeur maximale de la santé du joueur
        _healthSlider.maxValue = _playerHealth.GetMaxHealth();
        _healthSlider.value = _playerHealth.GetCurrentHealth();
    }

    void Update()
    {
        // Met à jour la barre de vie en fonction de la santé actuelle du joueur
        _healthSlider.value = _playerHealth.GetCurrentHealth();
    }
}
