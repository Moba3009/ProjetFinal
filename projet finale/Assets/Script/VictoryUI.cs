using UnityEngine;

public class VictoryUI : MonoBehaviour
{
    [SerializeField] private GameObject victoryPanel;  // Panel de victoire
    [SerializeField] private GameObject gameUI;        // Panel de jeu
    [SerializeField] private GameObject player;        // R�f�rence au joueur

    public void ShowVictory()
    {
        victoryPanel.SetActive(true);  // Afficher l'UI de victoire
        Time.timeScale = 0;            // Mettre le jeu en pause
    }

    public void RestartLevel()
    {
        victoryPanel.SetActive(false);
        gameUI.SetActive(false);  // Masquer le jeu actuel
        player.transform.position = new Vector3(0, 0, 0);  // R�initialiser la position du joueur
        Time.timeScale = 1;       // Reprendre le jeu
    }
}
