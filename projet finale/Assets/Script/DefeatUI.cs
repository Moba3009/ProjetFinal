using UnityEngine;

public class DefeatUI : MonoBehaviour
{
    [SerializeField] private GameObject _defeatPanel;  // Panel de défaite
    [SerializeField] private PlayerHealth _playerHealth;  // Référence au script PlayerHealth

    // Afficher l'UI de défaite
    public void ShowDefeatUI()
    {
        _defeatPanel.SetActive(true);  // Activer l'UI de défaite
    }

    // Recommencer le niveau
    public void RestartLevel()
    {
        _defeatPanel.SetActive(false);  // Masquer l'UI de défaite
        _playerHealth.RestartLevel();   // Recommencer la scène
    }
}
