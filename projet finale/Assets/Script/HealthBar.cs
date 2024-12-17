using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;  // R�f�rence � la barre de vie (Slider)
    [SerializeField] private PlayerHealth _playerHealth;  // R�f�rence au script PlayerHealth

    void Start()
    {
        // Initialise la barre de vie avec la valeur maximale de la sant� du joueur
        _healthSlider.maxValue = _playerHealth.GetMaxHealth();
        _healthSlider.value = _playerHealth.GetCurrentHealth();
    }

    void Update()
    {
        // Met � jour la barre de vie en fonction de la sant� actuelle du joueur
        _healthSlider.value = _playerHealth.GetCurrentHealth();
    }
}
