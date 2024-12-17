using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform _doorTransform;  // R�f�rence � la porte � ouvrir
    [SerializeField] private Vector3 _openPosition;  // Position de la porte lorsqu'elle est ouverte

    public void OpenDoor()
    {
        // Animation de l'ouverture de la porte
        _doorTransform.position = _openPosition;
        Debug.Log("La porte est ouverte !");
    }
}
