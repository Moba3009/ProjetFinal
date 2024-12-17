using UnityEngine;

public class VictoryTrigger : MonoBehaviour
{
    [SerializeField] private VictoryUI victoryUI;  // R�f�rence � l'UI de victoire

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Si le joueur entre en collision avec l'objet de victoire
        {
            victoryUI.ShowVictory();  // Affiche l'UI de victoire
        }
    }
}
