using UnityEngine;

public class DefeatUI : MonoBehaviour
{
    [SerializeField] private GameObject _defeatPanel;  // Panel de d�faite
    [SerializeField] private PlayerHealth _playerHealth;  // R�f�rence au script PlayerHealth

    // Afficher l'UI de d�faite
    public void ShowDefeatUI()
    {
        _defeatPanel.SetActive(true);  // Activer l'UI de d�faite
    }

    // Recommencer le niveau
    public void RestartLevel()
    {
        _defeatPanel.SetActive(false);  // Masquer l'UI de d�faite
        _playerHealth.RestartLevel();   // Recommencer la sc�ne
    }
}
