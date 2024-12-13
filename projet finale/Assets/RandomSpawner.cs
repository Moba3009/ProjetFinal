using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [Header("Liste de positions possibles")]
    [SerializeField] private List<Transform> _positions;  // Liste des 5 positions possibles

    [Header("Éléments à instancier")]
    [SerializeField] private GameObject _enemyRangedPrefab;  // Ennemi à distance
    [SerializeField] private GameObject _enemyMeleePrefab;   // Ennemi de mêlée
    [SerializeField] private GameObject _interactiveObjectPrefab; // Objet interactif

    // Start is called before the first frame update
    void Start()
    {
        SpawnElements();
    }

    // Fonction de génération aléatoire des éléments
    private void SpawnElements()
    {
        // Vérifier qu'il y a bien 5 positions dans la liste
        if (_positions.Count < 5)
        {
            Debug.LogError("Il faut au moins 5 positions !");
            return;
        }

        // Créer une liste d'indices pour la gestion des positions
        List<int> availablePositions = new List<int> { 0, 1, 2, 3, 4 };

        // Mélanger les positions (cela va permettre de placer les éléments de manière aléatoire)
        ShuffleList(availablePositions);

        // Instancier les éléments aux positions sélectionnées
        InstantiateElement(availablePositions[0], _enemyRangedPrefab);  // Ennemi à distance
        InstantiateElement(availablePositions[1], _enemyMeleePrefab);   // Ennemi de mêlée
        InstantiateElement(availablePositions[2], _interactiveObjectPrefab);  // Objet interactif

        // Instancier l'objet interactif aux positions restantes
        for (int i = 3; i < availablePositions.Count; i++)
        {
            InstantiateElement(availablePositions[i], _interactiveObjectPrefab);
        }
    }

    // Fonction d'instanciation d'un élément
    private void InstantiateElement(int positionIndex, GameObject prefab)
    {
        if (prefab != null)
        {
            Instantiate(prefab, _positions[positionIndex].position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Prefab manquant!");
        }
    }

    // Fonction pour mélanger les indices de positions
    private void ShuffleList(List<int> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int temp = list[i];
            int randomIndex = Random.Range(0, i + 1);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
