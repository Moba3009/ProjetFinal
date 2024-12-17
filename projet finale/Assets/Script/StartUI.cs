using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;  // Panel de démarrage
    [SerializeField] private GameObject gameUI;     // Panel du jeu

    public void StartGame()
    {
        startPanel.SetActive(false);  // Cache le panel de démarrage
        gameUI.SetActive(true);       // Affiche le panel du jeu (si nécessaire)
        Time.timeScale = 1;           // Assure que le jeu commence
    }

    public void QuitGame()
    {
        Application.Quit();  // Quitte l'application
    }
}
