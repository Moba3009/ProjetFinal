using UnityEngine;

public class Lever : MonoBehaviour
{
    [Header("Porte � Ouvrir")]
    [SerializeField] private GameObject _door;  // La porte � ouvrir
    [SerializeField] private float _rotationSpeed = 3f;  // Vitesse de la rotation de la porte
    [SerializeField] private float _openAngle = 90f;  // Angle d'ouverture de la porte
    [SerializeField] private bool _isActivated = false;  // �tat du levier

    private Quaternion _closedRotation;
    private Quaternion _openRotation;

    // Start est appel� au d�but
    void Start()
    {
        // Sauvegarder l'�tat initial de la porte
        _closedRotation = _door.transform.rotation;
        _openRotation = Quaternion.Euler(0, _openAngle, 0) * _closedRotation; // Rotation de la porte pour l'ouvrir
    }

    // Fonction appel�e quand le joueur entre dans la zone du levier
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Si c'est le joueur
        {
            ToggleLever();  // Active ou d�sactive le levier
        }
    }

    private void ToggleLever()
    {
        _isActivated = !_isActivated;  // Inverser l'�tat du levier
    }

    // Update est appel� � chaque frame
    void Update()
    {
        if (_isActivated)
        {
            // Si activ�, faire pivoter la porte jusqu'� l'angle d'ouverture
            _door.transform.rotation = Quaternion.Slerp(_door.transform.rotation, _openRotation, Time.deltaTime * _rotationSpeed);
        }
        else
        {
            // Si d�sactiv�, revenir � la position ferm�e
            _door.transform.rotation = Quaternion.Slerp(_door.transform.rotation, _closedRotation, Time.deltaTime * _rotationSpeed);
        }
    }
}
