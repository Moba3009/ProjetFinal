using UnityEngine;

public class Lever : MonoBehaviour
{
    [Header("Porte à Ouvrir")]
    [SerializeField] private GameObject _door;  // La porte à ouvrir
    [SerializeField] private float _rotationSpeed = 3f;  // Vitesse de la rotation de la porte
    [SerializeField] private float _openAngle = 90f;  // Angle d'ouverture de la porte
    [SerializeField] private bool _isActivated = false;  // État du levier

    private Quaternion _closedRotation;
    private Quaternion _openRotation;

    // Start est appelé au début
    void Start()
    {
        // Sauvegarder l'état initial de la porte
        _closedRotation = _door.transform.rotation;
        _openRotation = Quaternion.Euler(0, _openAngle, 0) * _closedRotation; // Rotation de la porte pour l'ouvrir
    }

    // Fonction appelée quand le joueur entre dans la zone du levier
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Si c'est le joueur
        {
            ToggleLever();  // Active ou désactive le levier
        }
    }

    private void ToggleLever()
    {
        _isActivated = !_isActivated;  // Inverser l'état du levier
    }

    // Update est appelé à chaque frame
    void Update()
    {
        if (_isActivated)
        {
            // Si activé, faire pivoter la porte jusqu'à l'angle d'ouverture
            _door.transform.rotation = Quaternion.Slerp(_door.transform.rotation, _openRotation, Time.deltaTime * _rotationSpeed);
        }
        else
        {
            // Si désactivé, revenir à la position fermée
            _door.transform.rotation = Quaternion.Slerp(_door.transform.rotation, _closedRotation, Time.deltaTime * _rotationSpeed);
        }
    }
}
